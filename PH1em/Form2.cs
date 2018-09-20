/*

      BRUNO DE ALMEIDA ZAMPIROM
       CIÊNCIAS DA COMPUTAÇÃO
    PROFESSOR: MARCOS JOSÉ BRUSSO
     DATA DE CRIAÇÃO: 16/09/2017

*/
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.IO; //Usada para a input/output de streams
namespace PH1em
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        public struct Br //Struct para Operador(mnemônico) e Operando(byte)
        {
            public string OPERADOR;
            public byte OPERANDO;
        }

        public struct Au //Struct para valor na Memória, contendo os valores e um flag para caso o valor seja alterado...
        {
            public byte valor;
            public bool flag;
        }

        public static void Exmem(ref byte ac, ref byte pc, ref byte cont, ref Br[] saida, ref Au[] MEM)
        {
            try //Usado em caso de erro de digitação e possível erro do programa, para evitar travamento do programa... 
            {
                while (true) //Ler até encontrar um HLT (ou ao encontrar um erro)...
                {
                    if (MEM[pc].valor == 0) //NOP
                        saida[cont].OPERADOR = "NOP";
                    else if (MEM[pc].valor.ToString("x") == "10") //LDR
                    {
                        pc++; //com operando incrementa o pc
                        ac = MEM[MEM[pc].valor].valor;
                        saida[cont].OPERADOR = "LDR";
                        saida[cont].OPERANDO = MEM[pc].valor;
                    }
                    else if (MEM[pc].valor.ToString("x") == "20")  //STR
                    {
                        pc++; //com operando incrementa o pc
                        MEM[MEM[pc].valor].valor = ac;
                        MEM[MEM[pc].valor].flag = true; //Valor Modificado, ativa o flag
                        saida[cont].OPERADOR = "STR";
                        saida[cont].OPERANDO = MEM[pc].valor;
                    }
                    else if (MEM[pc].valor.ToString("x") == "30") //ADD
                    {
                        pc++; //com operando incrementa o pc
                        ac += MEM[MEM[pc].valor].valor; //AC <- AC + MEM[end]
                        saida[cont].OPERADOR = "ADD";
                        saida[cont].OPERANDO = MEM[pc].valor;
                    }
                    else if (MEM[pc].valor.ToString("x") == "40") //SUB
                    {
                        pc++; //com operando incrementa o pc
                        ac -= MEM[MEM[pc].valor].valor; //AC <- AC - MEM[end]
                        saida[cont].OPERADOR = "SUB";
                        saida[cont].OPERANDO = MEM[pc].valor;
                    }
                    else if (MEM[pc].valor.ToString("x") == "50") //MUL
                    {
                        pc++; //com operando incrementa o pc
                        ac *= MEM[MEM[pc].valor].valor; //AC <- AC * MEM[end]
                        saida[cont].OPERADOR = "MUL";
                        saida[cont].OPERANDO = MEM[pc].valor;
                    }
                    else if (MEM[pc].valor.ToString("x") == "60") //DIV
                    {
                        pc++; //com operando incrementa o pc
                        ac /= MEM[MEM[pc].valor].valor; //AC <- AC / MEM[end]
                        saida[cont].OPERADOR = "DIV";
                        saida[cont].OPERANDO = MEM[pc].valor;
                    }
                    else if (MEM[pc].valor.ToString("x") == "70") //NOT
                    {
                        ac = (byte)(~ac); //AC <- !AC
                        saida[cont].OPERADOR = "NOT";
                    }
                    else if (MEM[pc].valor.ToString("x") == "80") //AND
                    {
                        pc++; //com operando incrementa o pc
                        ac = (byte)(ac & MEM[MEM[pc].valor].valor);//AC <- AC & MEM[end]
                        saida[cont].OPERADOR = "AND";
                        saida[cont].OPERANDO = MEM[pc].valor;
                    }
                    else if (MEM[pc].valor.ToString("x") == "90") //OR
                    {
                        pc++; //com operando incrementa o pc
                        ac = (byte)(ac | MEM[MEM[pc].valor].valor); //AC <- AC | MEM[end]
                        saida[cont].OPERADOR = "OR";
                        saida[cont].OPERANDO = MEM[pc].valor;
                    }
                    else if (MEM[pc].valor.ToString("x") == "a0") //XOR
                    {
                        pc++; //com operando incrementa o pc
                        ac = (byte)(ac ^ MEM[MEM[pc].valor].valor); //AC <- AC ^ MEM[end]
                        saida[cont].OPERADOR = "XOR";
                        saida[cont].OPERANDO = MEM[pc].valor;
                    }
                    else if (MEM[pc].valor.ToString("x") == "b0") //JMP
                    {
                        pc++; //com operando incrementa o pc
                        saida[cont].OPERADOR = "JMP";
                        pc = saida[cont].OPERANDO = MEM[pc].valor; //pc recebo o valor do Operando
                        goto fim;
                    }
                    else if (MEM[pc].valor.ToString("x") == "c0") //JEQ
                    {
                        pc++; //com operando incrementa o pc
                        saida[cont].OPERADOR = "JEQ";
                        saida[cont].OPERANDO = MEM[pc].valor;
                        if (ac == 0) //caso o número seja 0...
                        {
                            pc = MEM[pc].valor; //pc recebo o valor do Operando
                            goto fim; //Vai para fim sem atualizar o PC
                        }
                    }
                    else if (MEM[pc].valor.ToString("x") == "d0") //JG
                    {
                        pc++; //com operando incrementa o pc
                        saida[cont].OPERADOR = "JG";
                        saida[cont].OPERANDO = MEM[pc].valor;
                        if (ac > 0 && ac < 128) //caso o numero não seja zero e o primeiro bit não seja 1 ou seja positivo... 
                        {
                            pc = MEM[pc].valor; //pc recebo o valor do Operando
                            goto fim; //Vai para fim sem atualizar o PC
                        }
                    }
                    else if (MEM[pc].valor.ToString("x") == "e0") //JL
                    {
                        pc++; //com operando incrementa o pc
                        saida[cont].OPERADOR = "JL";
                        saida[cont].OPERANDO = MEM[pc].valor;
                        if (ac > 127) //caso o primeiro bit esteja "ligado" ou seja o número é negativo... 
                        {
                            pc = MEM[pc].valor; //pc recebo o valor do Operando
                            goto fim; //Vai para fim sem atualizar o PC
                        }
                    }
                    else if (MEM[pc].valor.ToString("x") == "f0") //HLT
                    {
                        pc++;
                        saida[cont].OPERADOR = "HLT";
                        cont++; //acrescenta o pc e o cont
                        break;
                    }
                    pc++; //acrescenta o pc
                    fim:;
                    cont++; //acrescenta o cont
                }
            }
            catch { }
        } //Fução principal(executa as operaçoes da MEM)

        private void button2_Click_1(object sender, EventArgs e)
        {
            //step 1: iniciando MEM
            Au[] MEM = new Au[255];
            for (byte i = 0; i < 255; ++i)
                MEM[i].valor = 0;

            //step 2: Inserindo valores obtidos no textbox1 na memória (MEM)
            string[] ab = textBox1.Text.Split('\n'); //vetor recebe a linha lida no textbox
            foreach (string ah in ab)
            {
                string[] ax = ah.Split(' '); //vetor recebe os dois valores da linha separados por espaço
                try
                { MEM[Convert.ToByte(ax[0], 16)].valor = byte.Parse(ax[1], System.Globalization.NumberStyles.HexNumber); } //Primeiro byte representa o endereço na memória, segundo representa o valor;
                catch
                { }
            }
            //step 3: Executando as operações na memória
            byte pc = 0; //iniciando Program Counter
            byte ac = 0; //Iniciando Acumulador
            byte cont = 0; //Contador para inserção no vetor 'saida'
            Br[] saida = new Br[255]; //vetor saida para os mnemônicos e endereços
            Exmem(ref ac, ref pc, ref cont, ref saida, ref MEM); //função de operação

            //step 4: Saída do arquivo...
            textBox2.ResetText();
            for (int i = 0; i < cont; ++i)
            {
                if (saida[i].OPERADOR != "NOP" && saida[i].OPERADOR != "NOT" && saida[i].OPERADOR != "HLT") //Caso não seja NOP,NOT,HLT escreve com operando, caso contrário apenas mnemônico
                    textBox2.Text += saida[i].OPERADOR + " " + saida[i].OPERANDO.ToString("X2") + System.Environment.NewLine; //Escrevendo os dados do vetor que contém o Operador(mnemônico) e Operando(hex)
                else
                    textBox2.Text += saida[i].OPERADOR + System.Environment.NewLine;
            }
            textBox2.Text += System.Environment.NewLine; //quebra de linha
            textBox2.Text += "AC " + ac.ToString("X2") + System.Environment.NewLine; //Escrevendo o ac(hex)
            textBox2.Text += "PC " + pc.ToString("X2") + System.Environment.NewLine; //Escrevendo o pc(hex)
            for (byte i = 0; i < 255; ++i)
            {
                if (MEM[i].flag) //Caso o valor foi alterado escreve, se flag==true
                    textBox2.Text += i.ToString("X") + " " + MEM[i].valor.ToString("X2") + System.Environment.NewLine;
            }
        }

        private void button1_Click(object sender, EventArgs e) //Arquivo de save
        {
            if(textBox2.TextLength>0)
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog(); //Chama o SaveFileDialog
                saveFileDialog1.Filter = "Documento de Texto|*.txt"; //exclusivamente .txt
                saveFileDialog1.Title = "Save File";
                saveFileDialog1.ShowDialog();
                if (saveFileDialog1.FileName != "") // Caso o nome do arquivo não esteja vazio..
                {
                    StreamWriter stw = new StreamWriter(saveFileDialog1.FileName); //Criando o arquivo de saida com base no end do save
                    stw.Write(textBox2.Text); //Escrevendo o Textbox2 no aquivo save...
                    stw.Close();
                    MessageBox.Show("Arquivo salvo com sucesso!");
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog(); //Chama o SaveFileDialog
            openFileDialog1.Filter = "Documento de Texto|*.txt"; //exclusivamente .txt
            openFileDialog1.Title = "Import File";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamReader vr = new StreamReader(openFileDialog1.FileName); //Endereço obtido com o Open File Dialog...
                textBox1.Text = vr.ReadToEnd();
                vr.Close();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowColor = true;

            fontDialog1.Font = textBox1.Font;
            fontDialog1.Color = textBox1.ForeColor;
            fontDialog1.Font = textBox2.Font;
            fontDialog1.Color = textBox2.ForeColor;

            if (fontDialog1.ShowDialog() != DialogResult.Cancel)
            {
                textBox1.Font = fontDialog1.Font;
                textBox1.ForeColor = fontDialog1.Color;
                textBox2.Font = fontDialog1.Font;
                textBox2.ForeColor = fontDialog1.Color;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.BackColor = colorDialog1.Color;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                button1.BackColor = colorDialog1.Color;
                button2.BackColor = colorDialog1.Color;
                button3.BackColor = colorDialog1.Color;
                button4.BackColor = colorDialog1.Color;
                button5.BackColor = colorDialog1.Color;
                button6.BackColor = colorDialog1.Color;
                button7.BackColor = colorDialog1.Color;
                button8.BackColor = colorDialog1.Color;
                button9.BackColor = colorDialog1.Color;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.BackColor = colorDialog1.Color;
                textBox2.BackColor = colorDialog1.Color;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Center")
            {
                textBox1.TextAlign = HorizontalAlignment.Center;
                textBox2.TextAlign = HorizontalAlignment.Center;
            }
            if (comboBox1.Text == "Left")
            {
                textBox1.TextAlign = HorizontalAlignment.Left;
                textBox2.TextAlign = HorizontalAlignment.Left;
            }
            if (comboBox1.Text == "Right")
            {
                textBox1.TextAlign = HorizontalAlignment.Right;
                textBox2.TextAlign = HorizontalAlignment.Right;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.linkLabel1.LinkVisited = true;

            // Navigate to a URL.
            System.Diagnostics.Process.Start("mailto:158788@upf.br");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            MessageBox.Show(" Universidade de Passo Fundo - 2017" + System.Environment.NewLine +
                            " Trabalho Emulador de PH1" + System.Environment.NewLine +
                            " Bruno de Almeida Zampirom" + System.Environment.NewLine +
                            " Marcos José Brusso" + System.Environment.NewLine +
                            " Arquitetura e Organização de Computadores" + System.Environment.NewLine +
                            " Ciências da Computação - ICEG"
            , "About PH1emu",
            MessageBoxButtons.OK,
            MessageBoxIcon.Question);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (textBox2.TextLength > 0)
                PrintTextBoxContent();
        }

        private void PrintTextBoxContent() //Código obtido no em: https://stackoverflow.com/questions/20019265/printing-a-document
        {
            #region Printer Selection
            PrintDialog printDlg = new PrintDialog();
            #endregion

            #region Create Document
            PrintDocument printDoc = new PrintDocument();
            printDoc.DocumentName = "Print Document";
            printDoc.PrintPage += printDoc_PrintPage;
            printDlg.Document = printDoc;
            #endregion

            if (printDlg.ShowDialog() == DialogResult.OK)
                printDoc.Print();
        }

        void printDoc_PrintPage(object sender, PrintPageEventArgs e)  //Código obtido no em: https://stackoverflow.com/questions/20019265/printing-a-document
        {
            e.Graphics.DrawString("Entrada:"+ System.Environment.NewLine + 
                                  this.textBox1.Text + System.Environment.NewLine+ System.Environment.NewLine +
                                  "Saida:" + System.Environment.NewLine +
                                  this.textBox2.Text
                                  , this.textBox2.Font, Brushes.Black, 10, 25);
        }

        private void Form2_Load_1(object sender, EventArgs e)
        {

        }
    }
}
