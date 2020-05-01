using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paging_System_KDH
{
    public partial class Form1 : Form
    {
        private const int multiprogrmming_degree = 4; //최대로 프로세스를 활성화 가능한 정도는 4임.
        private Label[] page_frame; //page_frame 라벨
        private DataGridView[] DataGridView_table;
        Stack<int> empty_page_frame = new Stack<int>(); //빈 페이지 프레임 관리 스택
        int[] frame_fifo;
        int fifo_index = 0;
        public Form1()
        {
            InitializeComponent();
            page_frame = new Label[] { page0, page1, page2, page3, page4, page5, page6 };
            DataGridView_table = new DataGridView[] { dataGridView1, dataGridView2, dataGridView3, dataGridView4 };
            frame_fifo = new int[] { 0, 1, 2, 3, 4, 5, 6 };
            for (int i=0; i< page_frame.Length; i++)
            {
                page_frame[i].Text = "NULL";
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add("page 01", 0, "-");
            dataGridView1.Rows.Add("page 02", 0, "-");
            dataGridView1.Rows.Add("page 03", 0, "-");
            dataGridView1.Rows.Add("page 04", 0, "-");
            dataGridView1.Rows.Add("page 05", 0, "-");
            dataGridView1.Rows.Add("page 06", 0, "-");
            dataGridView1.Rows.Add("page 07", 0, "-");
            dataGridView1.Rows.Add("page 08", 0, "-");

            dataGridView2.Rows.Add("page 11", 0, "-");
            dataGridView2.Rows.Add("page 21", 0, "-");
            dataGridView2.Rows.Add("page 31", 0, "-");
            dataGridView2.Rows.Add("page 41", 0, "-");
            dataGridView2.Rows.Add("page 51", 0, "-");
            dataGridView2.Rows.Add("page 61", 0, "-");



            dataGridView3.Rows.Add("page 12", 0, "-");
            dataGridView3.Rows.Add("page 22", 0, "-");
            dataGridView3.Rows.Add("page 32", 0, "-");
            dataGridView3.Rows.Add("page 42", 0, "-");
            dataGridView3.Rows.Add("page 52", 0, "-");
            dataGridView3.Rows.Add("page 62", 0, "-");
            dataGridView3.Rows.Add("page 72", 0, "-");



            dataGridView4.Rows.Add("page 13", 0, "-");
            dataGridView4.Rows.Add("page 23", 0, "-");
            dataGridView4.Rows.Add("page 33", 0, "-");


            empty_page_frame.Push(6);
            empty_page_frame.Push(5);
            empty_page_frame.Push(4);
            empty_page_frame.Push(3);
            empty_page_frame.Push(2);
            empty_page_frame.Push(1);
            empty_page_frame.Push(0);
            //빈 프레임은 6,4,2,0,5,3,1 순으로 저장되어있음.
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text!="" && Convert.ToInt32(textBox1.Text) - 1<dataGridView1.Rows.Count - 1 && Convert.ToInt32(textBox1.Text) - 1 >=0) //텍스트 박스가 비어있지 않고 0<textbox<8 경우
            {
                if (empty_page_frame.Count != 0) //빈 페이지가 있을 경우
                {
                    if (dataGridView1.Rows[Convert.ToInt32(textBox1.Text) - 1].Cells[1].Value.ToString() == "0")
                    {
                        dataGridView1.Rows[Convert.ToInt32(textBox1.Text) - 1].Cells[2].Value = empty_page_frame.Pop().ToString(); //스택에서 pop 시켜서 프레임 넣어줌
                        dataGridView1.Rows[Convert.ToInt32(textBox1.Text) - 1].Cells[1].Value = "1"; //활성화 된 상태를 나타냄 (레지던스 비트)
                        page_frame[Convert.ToInt32(dataGridView1.Rows[Convert.ToInt32(textBox1.Text) - 1].Cells[2].Value.ToString())].BackColor = Color.Yellow; //해당 프레임 색깔을 Yellow로
                        page_frame[Convert.ToInt32(dataGridView1.Rows[Convert.ToInt32(textBox1.Text) - 1].Cells[2].Value.ToString())].Text = dataGridView1.Rows[Convert.ToInt32(textBox1.Text) - 1].Cells[0].Value.ToString(); //해당 프레임 텍스트를 바꿈
                    }
                }

                else //빈 페이지가 없을 경우 페이지 교체 알고리즘 적용
                {
                    String prev_page = page_frame[frame_fifo[fifo_index]].Text; //바꾸고자하는 프레임의 페이지 이름을 저장

                    for(int i=0; i<DataGridView_table.Length; i++)
                    {
                        for(int j=0; j<DataGridView_table[i].Rows.Count-1; j++)
                        {
                            if(DataGridView_table[i].Rows[j].Cells[0].Value.ToString()==prev_page)
                            {
                                DataGridView_table[i].Rows[j].Cells[1].Value = 0;
                                DataGridView_table[i].Rows[j].Cells[2].Value = "-";
                            }
                        }
                    }

                    if (dataGridView1.Rows[Convert.ToInt32(textBox1.Text) - 1].Cells[1].Value.ToString() == "0")
                    {
                        dataGridView1.Rows[Convert.ToInt32(textBox1.Text) - 1].Cells[2].Value = frame_fifo[fifo_index]; 
                        dataGridView1.Rows[Convert.ToInt32(textBox1.Text) - 1].Cells[1].Value = "1"; //활성화 된 상태를 나타냄 (레지던스 비트)
                        page_frame[Convert.ToInt32(dataGridView1.Rows[Convert.ToInt32(textBox1.Text) - 1].Cells[2].Value.ToString())].BackColor = Color.Yellow; //해당 프레임 색깔을 Yellow로
                        page_frame[Convert.ToInt32(dataGridView1.Rows[Convert.ToInt32(textBox1.Text) - 1].Cells[2].Value.ToString())].Text = dataGridView1.Rows[Convert.ToInt32(textBox1.Text) - 1].Cells[0].Value.ToString(); //해당 프레임 텍스트를 바꿈
                        fifo_index = (fifo_index + 1) % 7;
                    }
                }
            }
            textBox1.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "" && Convert.ToInt32(textBox2.Text) - 1 < dataGridView2.Rows.Count - 1 && Convert.ToInt32(textBox2.Text) - 1 >= 0) //텍스트 박스가 비어있지 않고 0<textbox<8 경우
            {
                if (empty_page_frame.Count != 0) //빈 페이지가 있을 경우
                {
                    if (dataGridView2.Rows[Convert.ToInt32(textBox2.Text) - 1].Cells[2].Value.ToString() == "-")
                    {
                        dataGridView2.Rows[Convert.ToInt32(textBox2.Text) - 1].Cells[2].Value = empty_page_frame.Pop().ToString(); //스택에서 pop 시켜서 프레임 넣어줌
                        dataGridView2.Rows[Convert.ToInt32(textBox2.Text) - 1].Cells[1].Value = "1"; //활성화 된 상태를 나타냄 (레지던스 비트)
                        page_frame[Convert.ToInt32(dataGridView2.Rows[Convert.ToInt32(textBox2.Text) - 1].Cells[2].Value.ToString())].BackColor = Color.Green; //해당 프레임 색깔을 Green로
                        page_frame[Convert.ToInt32(dataGridView2.Rows[Convert.ToInt32(textBox2.Text) - 1].Cells[2].Value.ToString())].Text = dataGridView2.Rows[Convert.ToInt32(textBox2.Text) - 1].Cells[0].Value.ToString(); //해당 프레임 텍스트를 바꿈
                    }
                }
                else
                {
                    String prev_page = page_frame[frame_fifo[fifo_index]].Text; //바꾸고자하는 프레임의 페이지 이름을 저장

                    for (int i = 0; i < DataGridView_table.Length; i++)
                    {
                        for (int j = 0; j < DataGridView_table[i].Rows.Count - 1; j++)
                        {
                            if (DataGridView_table[i].Rows[j].Cells[0].Value.ToString() == prev_page)
                            {
                                DataGridView_table[i].Rows[j].Cells[1].Value = 0;
                                DataGridView_table[i].Rows[j].Cells[2].Value = "-";
                            }
                        }
                    }

                    if (dataGridView2.Rows[Convert.ToInt32(textBox2.Text) - 1].Cells[1].Value.ToString() == "0")
                    {
                        dataGridView2.Rows[Convert.ToInt32(textBox2.Text) - 1].Cells[2].Value = frame_fifo[fifo_index];
                        dataGridView2.Rows[Convert.ToInt32(textBox2.Text) - 1].Cells[1].Value = "1"; //활성화 된 상태를 나타냄 (레지던스 비트)
                        page_frame[Convert.ToInt32(dataGridView2.Rows[Convert.ToInt32(textBox2.Text) - 1].Cells[2].Value.ToString())].BackColor = Color.Green; //해당 프레임 색깔을 Yellow로
                        page_frame[Convert.ToInt32(dataGridView2.Rows[Convert.ToInt32(textBox2.Text) - 1].Cells[2].Value.ToString())].Text = dataGridView2.Rows[Convert.ToInt32(textBox2.Text) - 1].Cells[0].Value.ToString(); //해당 프레임 텍스트를 바꿈
                        fifo_index = (fifo_index + 1) % 7;
                    }
                }
            }
            textBox2.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != "" && Convert.ToInt32(textBox3.Text) - 1 < dataGridView3.Rows.Count - 1 && Convert.ToInt32(textBox3.Text) - 1 >= 0) //텍스트 박스가 비어있지 않고 0<textbox<8 경우
            {
                if (empty_page_frame.Count != 0) //빈 페이지가 있을 경우
                {
                    if (dataGridView3.Rows[Convert.ToInt32(textBox3.Text) - 1].Cells[2].Value.ToString() == "-")
                    {
                        dataGridView3.Rows[Convert.ToInt32(textBox3.Text) - 1].Cells[2].Value = empty_page_frame.Pop().ToString(); //스택에서 pop 시켜서 프레임 넣어줌
                        dataGridView3.Rows[Convert.ToInt32(textBox3.Text) - 1].Cells[1].Value = "1"; //활성화 된 상태를 나타냄 (레지던스 비트)
                        page_frame[Convert.ToInt32(dataGridView3.Rows[Convert.ToInt32(textBox3.Text) - 1].Cells[2].Value.ToString())].BackColor = Color.Red; //해당 프레임 색깔을 Red로
                        page_frame[Convert.ToInt32(dataGridView3.Rows[Convert.ToInt32(textBox3.Text) - 1].Cells[2].Value.ToString())].Text = dataGridView3.Rows[Convert.ToInt32(textBox3.Text) - 1].Cells[0].Value.ToString(); //해당 프레임 텍스트를 바꿈
                    }
                }
                else
                {
                    String prev_page = page_frame[frame_fifo[fifo_index]].Text; //바꾸고자하는 프레임의 페이지 이름을 저장

                    for (int i = 0; i < DataGridView_table.Length; i++)
                    {
                        for (int j = 0; j < DataGridView_table[i].Rows.Count - 1; j++)
                        {
                            if (DataGridView_table[i].Rows[j].Cells[0].Value.ToString() == prev_page)
                            {
                                DataGridView_table[i].Rows[j].Cells[1].Value = 0;
                                DataGridView_table[i].Rows[j].Cells[2].Value = "-";
                            }
                        }
                    }

                    if (dataGridView3.Rows[Convert.ToInt32(textBox3.Text) - 1].Cells[1].Value.ToString() == "0")
                    {
                        dataGridView3.Rows[Convert.ToInt32(textBox3.Text) - 1].Cells[2].Value = frame_fifo[fifo_index];
                        dataGridView3.Rows[Convert.ToInt32(textBox3.Text) - 1].Cells[1].Value = "1"; //활성화 된 상태를 나타냄 (레지던스 비트)
                        page_frame[Convert.ToInt32(dataGridView3.Rows[Convert.ToInt32(textBox3.Text) - 1].Cells[2].Value.ToString())].BackColor = Color.Red; //해당 프레임 색깔을 Yellow로
                        page_frame[Convert.ToInt32(dataGridView3.Rows[Convert.ToInt32(textBox3.Text) - 1].Cells[2].Value.ToString())].Text = dataGridView3.Rows[Convert.ToInt32(textBox3.Text) - 1].Cells[0].Value.ToString(); //해당 프레임 텍스트를 바꿈
                        fifo_index = (fifo_index + 1) % 7;
                    }
                }
            }
            textBox3.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox4.Text != "" && Convert.ToInt32(textBox4.Text) - 1 < dataGridView4.Rows.Count - 1 && Convert.ToInt32(textBox4.Text) - 1 >= 0) //텍스트 박스가 비어있지 않고 0<textbox<8 경우
            {
                if (empty_page_frame.Count != 0) //빈 페이지가 있을 경우
                {
                    if (dataGridView4.Rows[Convert.ToInt32(textBox4.Text) - 1].Cells[2].Value.ToString() == "-")
                    {
                        dataGridView4.Rows[Convert.ToInt32(textBox4.Text) - 1].Cells[2].Value = empty_page_frame.Pop().ToString(); //스택에서 pop 시켜서 프레임 넣어줌
                        dataGridView4.Rows[Convert.ToInt32(textBox4.Text) - 1].Cells[1].Value = "1"; //활성화 된 상태를 나타냄 (레지던스 비트)
                        page_frame[Convert.ToInt32(dataGridView4.Rows[Convert.ToInt32(textBox4.Text) - 1].Cells[2].Value.ToString())].BackColor = Color.SkyBlue; //해당 프레임 색깔을 SkyBlue로
                        page_frame[Convert.ToInt32(dataGridView4.Rows[Convert.ToInt32(textBox4.Text) - 1].Cells[2].Value.ToString())].Text = dataGridView4.Rows[Convert.ToInt32(textBox4.Text) - 1].Cells[0].Value.ToString(); //해당 프레임 텍스트를 바꿈
                    }
                }
                else
                {
                    String prev_page = page_frame[frame_fifo[fifo_index]].Text; //바꾸고자하는 프레임의 페이지 이름을 저장

                    for (int i = 0; i < DataGridView_table.Length; i++)
                    {
                        for (int j = 0; j < DataGridView_table[i].Rows.Count - 1; j++)
                        {
                            if (DataGridView_table[i].Rows[j].Cells[0].Value.ToString() == prev_page)
                            {
                                DataGridView_table[i].Rows[j].Cells[1].Value = 0;
                                DataGridView_table[i].Rows[j].Cells[2].Value = "-";
                            }
                        }
                    }

                    if (dataGridView4.Rows[Convert.ToInt32(textBox4.Text) - 1].Cells[1].Value.ToString() == "0")
                    {
                        dataGridView4.Rows[Convert.ToInt32(textBox4.Text) - 1].Cells[2].Value = frame_fifo[fifo_index];
                        dataGridView4.Rows[Convert.ToInt32(textBox4.Text) - 1].Cells[1].Value = "1"; //활성화 된 상태를 나타냄 (레지던스 비트)
                        page_frame[Convert.ToInt32(dataGridView4.Rows[Convert.ToInt32(textBox4.Text) - 1].Cells[2].Value.ToString())].BackColor = Color.SkyBlue; //해당 프레임 색깔을 SkyBlue로
                        page_frame[Convert.ToInt32(dataGridView4.Rows[Convert.ToInt32(textBox4.Text) - 1].Cells[2].Value.ToString())].Text = dataGridView4.Rows[Convert.ToInt32(textBox4.Text) - 1].Cells[0].Value.ToString(); //해당 프레임 텍스트를 바꿈
                        fifo_index = (fifo_index + 1) % 7;
                    }
                }
            }
            textBox4.Text = "";
        }
    }
}
