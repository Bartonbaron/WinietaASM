; Struktura parametrów (Windows x64 ABI):
; rcx - wskaŸnik na dane obrazu (byte[])
; rdx - szerokoœæ obrazu (int)
; r8 - wysokoœæ obrazu (int)
; r9 - stride (int)
; [rsp+40] - intensity (float)
; [rsp+48] - threadCount (int)

.data
    align 16
    maxDistanceRecip REAL4 1.0, 1.0, 1.0, 1.0    ; 1/maxDistance x4
    centerX DWORD 0                               ; width/2
    centerY DWORD 0                              ; height/2
    intensity REAL4 0.0                          ; parametr intensity
    ones REAL4 1.0, 1.0, 1.0, 1.0               ; sta³e do porównañ x4
    zeros REAL4 0.0, 0.0, 0.0, 0.0
    increment DWORD 0, 1, 2, 3                   ; offset dla 4 kolejnych pikseli
    three DWORD 3                                ; mno¿nik dla RGB
    maxFloat REAL4 255.0                         ; maksymalna wartoœæ koloru

.code

public ApplyVignetteASM
ApplyVignetteASM proc frame
    push rbp
    .pushreg rbp
    mov rbp, rsp
    .setframe rbp, 0
    sub rsp, 96
    .allocstack 96
    
    movdqu [rsp+32], xmm6
    .savexmm128 xmm6, 32
    movdqu [rsp+48], xmm7
    .savexmm128 xmm7, 48
    movdqu [rsp+64], xmm8
    .savexmm128 xmm8, 64
    movdqu [rsp+80], xmm9
    .savexmm128 xmm9, 80
    .endprolog
    
    ; Zachowanie parametrów
    mov [rsp], rcx                     ; ptr do danych obrazu
    mov [rsp+8], rdx                   ; szerokoœæ
    mov [rsp+16], r8                   ; wysokoœæ
    mov [rsp+24], r9                   ; stride
    
    ; Obliczenie œrodka obrazu
    shr edx, 1
    mov [centerX], edx
    mov eax, r8d
    shr eax, 1
    mov [centerY], eax
    
    ; Przygotowanie wektora z intensity (4x ta sama wartoœæ)
    movss xmm0, real4 ptr [rsp+40]    ; intensity
    shufps xmm0, xmm0, 0              ; rozmno¿enie na wszystkie elementy
    movaps xmm7, xmm0                  ; zachowanie w xmm7
    
    ; Wczytanie wektora z przyrostami (0,1,2,3)
    movdqa xmm8, xmmword ptr [increment]
    
    ; G³ówna pêtla po wierszach
    xor r10, r10                       ; y = 0
row_loop:
    ; Konwersja y na wektor zmiennoprzecinkowy
    cvtsi2ss xmm1, r10d
    shufps xmm1, xmm1, 0              ; rozmno¿enie y na wszystkie elementy
    movaps xmm9, xmm1                  ; zachowanie y
    
    ; Inicjalizacja x (bêdziemy przetwarzaæ 4 piksele naraz)
    xor r11, r11                       ; x = 0
pixel_loop:
    ; Przygotowanie wektora x (x, x+1, x+2, x+3)
    cvtsi2ss xmm0, r11d
    shufps xmm0, xmm0, 0              ; x,x,x,x
    cvtdq2ps xmm1, xmm8               ; konwersja przyrostów 0,1,2,3
    addps xmm0, xmm1                   ; x+0, x+1, x+2, x+3
    
    ; Obliczenie dx (x - centerX) dla 4 pikseli
    cvtsi2ss xmm1, dword ptr [centerX]
    shufps xmm1, xmm1, 0
    subps xmm0, xmm1                   ; dx dla 4 pikseli
    
    ; Obliczenie dy (y - centerY) dla 4 pikseli
    movaps xmm1, xmm9                  ; y
    cvtsi2ss xmm2, dword ptr [centerY]
    shufps xmm2, xmm2, 0
    subps xmm1, xmm2                   ; dy dla 4 pikseli
    
    ; Obliczenie distance^2 = dx^2 + dy^2
    movaps xmm2, xmm0
    mulps xmm2, xmm2                   ; dx^2
    movaps xmm3, xmm1
    mulps xmm3, xmm3                   ; dy^2
    addps xmm2, xmm3                   ; dx^2 + dy^2
    
    ; Obliczenie sqrt(distance^2)
    sqrtps xmm2, xmm2                  ; sqrt dla 4 wartoœci naraz
    
    ; Obliczenie wspó³czynnika winiety dla 4 pikseli
    mulps xmm2, xmm7                   ; distance * intensity
    movaps xmm3, dword ptr [ones]
    subps xmm3, xmm2                   ; 1 - (distance * intensity)
    
    ; Ograniczenie wartoœci dla 4 pikseli
    maxps xmm3, dword ptr [zeros]
    minps xmm3, dword ptr [ones]
    
    ; Przetworzenie 4 pikseli
    mov rcx, 4                         ; licznik pikseli
pixel_process:
    ; Obliczenie indeksu piksela
    mov rax, r10                       ; y
    mul r9                             ; y * stride
    add rax, r11                       ; + x
    add rax, rcx                       ; + offset aktualnego piksela
    imul eax, [three]                  ; * 3 (RGB)
    add rax, [rsp]                     ; + base ptr
    
    ; Pobranie sk³adowej koloru i aplikacja efektu
    movzx edx, byte ptr [rax]          ; sk³adowa koloru
    cvtsi2ss xmm0, edx
    mulss xmm0, xmm3                   ; mno¿enie przez wspó³czynnik winiety
    cvttss2si edx, xmm0
    mov byte ptr [rax], dl             ; zapisanie wyniku
    
    dec rcx
    jnz pixel_process
    
    ; Przejœcie do nastêpnych 4 pikseli
    add r11, 4
    cmp r11d, dword ptr [rsp+8]        ; porównanie z szerokoœci¹
    jl pixel_loop
    
    ; Nastêpny wiersz
    inc r10
    cmp r10d, dword ptr [rsp+16]       ; porównanie z wysokoœci¹
    jl row_loop
    
    ; Epilog
    movdqu xmm6, [rsp+32]
    movdqu xmm7, [rsp+48]
    movdqu xmm8, [rsp+64]
    movdqu xmm9, [rsp+80]
    mov rsp, rbp
    pop rbp
    ret
ApplyVignetteASM endp

END