# PH1-EMULATOR

  Este projeto é um emulador, que executa as instruções da arquitetura hipotética PH1 codificadas em hexadecimal.

## Motivation

  A principal função do projeto é entender como funciona a execução das instruções da arquitetura, através de uma linguagem de alto nível. Também tem como objetivo, a apresentação e avaliação do mesmo na disciplina de Arquitetura e Organização de Computadores.

## Tests

  Os método de entrada, seja pelo ambiente ou por arquivo externo(em arquivo texto), devem ser escritos em hexadecimal. Apenas um espaço deve ser apresentado entre o endereço e o valor, linhas em branco, para a facilitação da compreenção do usuário, podem ser usadas. O programa tem um pequena prevenção de bugs, quando por exemplo, o usuário digita uma entrada inválida o programa ignora a linha com o erro.
  
### Example
  
  Deve ser informado o endereço, seguido de um espaço e depois o conteúdo (*ambos em hexadecimal*) 
  
`00 10`

`01 81`

`02 70`

`03 20`

`04 80`

`05 F0`

`80 00`

`81 F1`

  Esse código hexadecimal retornaria a seguinte saída:

`LDR 81` - `(Primeira operação, LDR = AC <- MEM[129])`

`NOT` - `(Segunda operação, NOT = AC <- !AC)`

`STR 80` - `(Terceira operação, STR = MEM[128]<-AC)`

`HLT` - `(Quarta e última operação HLT = Encerra o processo)`

`AC 0E` - `(Estado final do acumulador)`

`PC 06` - `(Estado final do program counter)`

`80 0E` - `(Endereços com valores alterados durante a execução)`

## Emulator operations

Todas as operações aceitas pelo emulador:

| Código Hex   | Instrução mnemônico    | Operando    |
|------|--------------------------------------|--------|
| 00   | NOP                                  | Não    |
| 10   | LDR                                  | Sim    |
| 20   | STR                                  | Sim    |
| 30   | ADD                                  | Sim    |
| 40   | SUB                                  | Sim    |
| 50   | MUL                                  | Sim    |
| 60   | DIV                                  | Sim    |
| 70   | NOT                                  | Não    |
| 80   | AND                                  | Sim    |
| 90   | OR                                   | Sim    |
| A0   | XOR                                  | Sim    |
| B0   | JMP                                  | Sim    |
| C0   | JEQ                                  | Sim    |
| D0   | JG                                   | Sim    |
| E0   | JL                                   | Sim    |
| F0   | HLT                                  | Não    |

## API Reference

  O ambiente de desenvolvimento do programa é o Visual Studio 2017, tendo como linguagem de programação ultilizada o C#. Aplicativo de Windows Forms (.NET FRAMEWORK). Todos os arquivos necessário foram criados pelo Visual Studio. Arquivos explícitamente modificados foram: Form2.cs e Form2.Designer.cs. Executável do programa encontra-se em: \PH1em\bin\Debug\PH1em.exe

## Developer
  Bruno de Almeida Zampirom 
  158788@upf.br

Universidade de Passo Fundo - ICEG
Ciências da Computação
