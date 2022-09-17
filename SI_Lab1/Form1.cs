using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SI_Lab1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            add_rows();
        }

        public void add_rows() {

            this.dataGridView1.Rows.Add();
            this.dataGridView1.Rows.Add();
            this.dataGridView1.Rows.Add();
            this.dataGridView2.Rows.Add();
            this.dataGridView2.Rows.Add();
            this.dataGridView2.Rows.Add();
            this.dataGridView2.Rows.Add();
            this.dataGridView3.Rows.Add();
            this.dataGridView3.Rows.Add();
            this.dataGridView3.Rows.Add();
            this.dataGridView3.Rows.Add();
            this.dataGridView3.Rows.Add();

            dataGridView1.Rows[0].Cells[0].Value = 0;
            dataGridView1.Rows[0].Cells[1].Value = 1;
            dataGridView1.Rows[0].Cells[2].Value = 9;
            dataGridView1.Rows[1].Cells[0].Value = 0.5;
            dataGridView1.Rows[1].Cells[1].Value = 2;
            dataGridView1.Rows[1].Cells[2].Value = 8;
            dataGridView1.Rows[2].Cells[0].Value = 1;
            dataGridView1.Rows[2].Cells[1].Value = 3;
            dataGridView1.Rows[2].Cells[2].Value = 4;
            dataGridView2.Rows[0].Cells[0].Value = 0;
            dataGridView2.Rows[0].Cells[1].Value = 1;
            dataGridView2.Rows[0].Cells[2].Value = 9;
            dataGridView2.Rows[1].Cells[0].Value = 0.5;
            dataGridView2.Rows[1].Cells[1].Value = 3;
            dataGridView2.Rows[1].Cells[2].Value = 6;
            dataGridView2.Rows[2].Cells[0].Value = 1;
            dataGridView2.Rows[2].Cells[1].Value = 4;
            dataGridView2.Rows[2].Cells[2].Value = 5;
            dataGridView2.Rows[3].Cells[0].Value = 0.2;
            dataGridView2.Rows[3].Cells[1].Value = 2;
            dataGridView2.Rows[3].Cells[2].Value = 7;


        }

        

       

        private void button1_Click(object sender, EventArgs e)
        {
            bool correct_values = true;
            bool rule_of_trap = true;
            bool have_one = false;
            bool have_null = false;

            for (int i = 0; i < 3; i++) { // проверка на то что в ячейках числа
                for (int j = 0; j < 3; j++) {
                    if (!double.TryParse(Convert.ToString(dataGridView1.Rows[i].Cells[j].Value), out double number))
                        correct_values = false;

                }
            }
            // проверка на то есть ли 0 и 1 в альфах
            for (int i = 0; i < 3; i++) {
                if (Convert.ToDouble(dataGridView1.Rows[i].Cells[0].Value) == 1)
                    have_one = true;
                if (Convert.ToDouble(dataGridView1.Rows[i].Cells[0].Value) == 0)
                    have_null = true;
            }

            if (!have_one || !have_null) {
                correct_values = false;
            }

            if (!correct_values) {
                MessageBox.Show("Заполните верно все ячейки таблицы!");
                return;
            }

            int[] min_max_a = new int[3]; // переменные для отсортировки a lvl по возрастанию
            int check_a_count = 0;
            int cheking_a_index = 0;
            bool[] a_was_check = new bool[3] { false, false, false };
            double min_a_value = 1000000;

            while (check_a_count < 3) { // индексы эй выстраиваются по возрастанию
                for (int i = 0; i < 3; i++) {
                    if (min_a_value > Convert.ToDouble(dataGridView1.Rows[i].Cells[0].Value) && a_was_check[i] == false) {
                        min_a_value = Convert.ToDouble(dataGridView1.Rows[i].Cells[0].Value); ////
                        cheking_a_index = i;
                       
                    }
                }
                min_max_a[check_a_count] = cheking_a_index;
                a_was_check[cheking_a_index] = true;
                min_a_value = 1000000;
                check_a_count++;
            }

            // проверяем то чтобы альфа срезы не повторялись 

            if (Convert.ToDouble(dataGridView1.Rows[0].Cells[0].Value) == Convert.ToDouble(dataGridView1.Rows[1].Cells[0].Value) || Convert.ToDouble(dataGridView1.Rows[1].Cells[0].Value) == Convert.ToDouble(dataGridView1.Rows[2].Cells[0].Value) || Convert.ToDouble(dataGridView1.Rows[0].Cells[0].Value) == Convert.ToDouble(dataGridView1.Rows[2].Cells[0].Value))
            { rule_of_trap = false; }

            if (Convert.ToDouble(dataGridView1.Rows[min_max_a[0]].Cells[1].Value) >= Convert.ToDouble(dataGridView1.Rows[min_max_a[1]].Cells[1].Value) || Convert.ToDouble(dataGridView1.Rows[min_max_a[0]].Cells[2].Value) <= Convert.ToDouble(dataGridView1.Rows[min_max_a[1]].Cells[2].Value)) // здесь начинаем проверять чтобы границы удовлетворяли условиям трапеции
            { rule_of_trap = false; }
            if (Convert.ToDouble(dataGridView1.Rows[min_max_a[1]].Cells[1].Value) >= Convert.ToDouble(dataGridView1.Rows[min_max_a[2]].Cells[1].Value) || Convert.ToDouble(dataGridView1.Rows[min_max_a[1]].Cells[2].Value) <= Convert.ToDouble(dataGridView1.Rows[min_max_a[2]].Cells[2].Value)) // здесь начинаем проверять чтобы границы удовлетворяли условиям трапеции
            { rule_of_trap = false; }

            if (!rule_of_trap) {
                MessageBox.Show("Некорректное заполнение таблицы A!");
            }

            else {

                this.chart1.Series[0].Points.Clear();

                bool[,] was_built = new bool[3, 3] { { false, false, false }, { false, false, false }, { false, false, false } };

                int build_i = 0, build_j = 0;

                int count_build_points = 0;

                double min_value = 10000000;

                while (count_build_points < 6) {

                    for (int i = 0; i < 3; i++) { // выцепляем минимальную точку для построения на графике

                        for (int j = 1; j < 3; j++)
                        {
                            if (min_value > Convert.ToDouble(dataGridView1.Rows[i].Cells[j].Value) && was_built[i, j] == false) {

                                min_value = Convert.ToDouble(dataGridView1.Rows[i].Cells[j].Value);
                                build_i = i;
                                build_j = j;
                            }
                        }
                    }

                    this.chart1.Series[0].Points.AddXY(min_value, Convert.ToDouble(dataGridView1.Rows[build_i].Cells[0].Value));
                    was_built[build_i, build_j] = true;
                    min_value = 10000000;
                    count_build_points++;
                }

            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool correct_values = true;
            bool rule_of_trap = true;
            bool have_one = false;
            bool have_null = false;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (!double.TryParse(Convert.ToString(dataGridView2.Rows[i].Cells[j].Value), out double number))
                        correct_values = false;
                }
            }

            // проверка на то есть ли 0 и 1 в альфах
            for (int i = 0; i < 4; i++)
            {
                if (Convert.ToDouble(dataGridView2.Rows[i].Cells[0].Value) == 1)
                    have_one = true;
                if (Convert.ToDouble(dataGridView2.Rows[i].Cells[0].Value) == 0)
                    have_null = true;
            }

            if (!have_one || !have_null)
            {
                correct_values = false;
            }

            if (!correct_values) {
                MessageBox.Show("Заполните верно все ячейки таблицы!");
                return;
            }

            int[] min_max_a = new int[4]; // переменные для отсортировки a lvl по возрастанию
            int check_a_count = 0;
            int cheking_a_index = 0;
            bool[] a_was_check = new bool[4] { false, false, false, false };
            double min_a_value = 1000000;

            while (check_a_count < 4)
            { // индексы эй выстраиваются по возрастанию
                for (int i = 0; i < 4; i++)
                {
                    if (min_a_value > Convert.ToDouble(dataGridView2.Rows[i].Cells[0].Value) && a_was_check[i] == false)
                    {
                        min_a_value = Convert.ToDouble(dataGridView2.Rows[i].Cells[0].Value); ////
                        cheking_a_index = i;

                    }
                }
                min_max_a[check_a_count] = cheking_a_index;
                a_was_check[cheking_a_index] = true;
                min_a_value = 1000000;
                check_a_count++;
            }

            if (Convert.ToDouble(dataGridView2.Rows[0].Cells[0].Value) == Convert.ToDouble(dataGridView2.Rows[1].Cells[0].Value) || Convert.ToDouble(dataGridView2.Rows[1].Cells[0].Value) == Convert.ToDouble(dataGridView2.Rows[2].Cells[0].Value) || Convert.ToDouble(dataGridView2.Rows[2].Cells[0].Value) == Convert.ToDouble(dataGridView2.Rows[3].Cells[0].Value) || Convert.ToDouble(dataGridView2.Rows[2].Cells[0].Value) == Convert.ToDouble(dataGridView2.Rows[0].Cells[0].Value) || Convert.ToDouble(dataGridView2.Rows[3].Cells[0].Value) == Convert.ToDouble(dataGridView2.Rows[0].Cells[0].Value) || Convert.ToDouble(dataGridView2.Rows[3].Cells[0].Value) == Convert.ToDouble(dataGridView2.Rows[1].Cells[0].Value))
            { rule_of_trap = false; }

            if (Convert.ToDouble(dataGridView2.Rows[min_max_a[0]].Cells[1].Value) >= Convert.ToDouble(dataGridView2.Rows[min_max_a[1]].Cells[1].Value) || Convert.ToDouble(dataGridView2.Rows[min_max_a[0]].Cells[2].Value) <= Convert.ToDouble(dataGridView2.Rows[min_max_a[1]].Cells[2].Value)) // здесь начинаем проверять чтобы границы удовлетворяли условиям трапеции
            { rule_of_trap = false; }
            if (Convert.ToDouble(dataGridView2.Rows[min_max_a[1]].Cells[1].Value) >= Convert.ToDouble(dataGridView2.Rows[min_max_a[2]].Cells[1].Value) || Convert.ToDouble(dataGridView2.Rows[min_max_a[1]].Cells[2].Value) <= Convert.ToDouble(dataGridView2.Rows[min_max_a[2]].Cells[2].Value)) // здесь начинаем проверять чтобы границы удовлетворяли условиям трапеции
            { rule_of_trap = false; }
            if (Convert.ToDouble(dataGridView2.Rows[min_max_a[2]].Cells[1].Value) >= Convert.ToDouble(dataGridView2.Rows[min_max_a[3]].Cells[1].Value) || Convert.ToDouble(dataGridView2.Rows[min_max_a[2]].Cells[2].Value) <= Convert.ToDouble(dataGridView2.Rows[min_max_a[3]].Cells[2].Value)) // здесь начинаем проверять чтобы границы удовлетворяли условиям трапеции
            { rule_of_trap = false; }

            if (!rule_of_trap)
            {
                MessageBox.Show("Некорректное заполнение таблицы B!");
            }

            else
            {

                this.chart1.Series[1].Points.Clear();

                bool[,] was_built = new bool[4, 3] { { false, false, false }, { false, false, false }, { false, false, false }, { false, false, false } };

                int build_i = 0, build_j = 0;

                int count_build_points = 0;

                double min_value = 10000000;

                while (count_build_points < 8)
                {

                    for (int i = 0; i < 4; i++)
                    { // выцепляем минимальную точку для построения на графике

                        for (int j = 1; j < 3; j++)
                        {
                            if (min_value > Convert.ToDouble(dataGridView2.Rows[i].Cells[j].Value) && was_built[i, j] == false)
                            {

                                min_value = Convert.ToDouble(dataGridView2.Rows[i].Cells[j].Value);
                                build_i = i;
                                build_j = j;
                            }
                        }
                    }

                    this.chart1.Series[1].Points.AddXY(min_value, Convert.ToDouble(dataGridView2.Rows[build_i].Cells[0].Value));
                    was_built[build_i, build_j] = true;
                    min_value = 10000000;
                    count_build_points++;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.chart1.Series[0].Points.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.chart1.Series[1].Points.Clear();
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Здоровенный скопированный код на проверку корректности введённых значений
            bool correct_values = true;
            bool rule_of_trap_A = true;
            bool have_one_A = false;
            bool have_null_A = false;
            bool have_one_B = false;
            bool have_null_B = false;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (!double.TryParse(Convert.ToString(dataGridView1.Rows[i].Cells[j].Value), out double number))
                        correct_values = false;
                }
            }

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (!double.TryParse(Convert.ToString(dataGridView2.Rows[i].Cells[j].Value), out double number))
                        correct_values = false;
                }
            }

            for (int i = 0; i < 3; i++)
            {
                if (Convert.ToDouble(dataGridView1.Rows[i].Cells[0].Value) == 1)
                    have_one_A = true;
                if (Convert.ToDouble(dataGridView1.Rows[i].Cells[0].Value) == 0)
                    have_null_A = true;
            }

            for (int i = 0; i < 4; i++)
            {
                if (Convert.ToDouble(dataGridView2.Rows[i].Cells[0].Value) == 1)
                    have_one_B = true;
                if (Convert.ToDouble(dataGridView2.Rows[i].Cells[0].Value) == 0)
                    have_null_B = true;
            }

            if (!have_one_B || !have_null_B)
            {
                correct_values = false;
            }

            if (!have_one_A || !have_null_A)
            {
                correct_values = false;
            }

            if (!correct_values)
            {
                MessageBox.Show("Некорректные данные!");
                return;
            }

            int[] min_max_a_A = new int[3]; // переменные для отсортировки a lvl по возрастанию
            int check_a_count_A = 0;
            int cheking_a_index_A = 0;
            bool[] a_was_check_A = new bool[3] { false, false, false };
            double min_a_value_A = 1000000;

            while (check_a_count_A < 3)
            { // индексы эй выстраиваются по возрастанию
                for (int i = 0; i < 3; i++)
                {
                    if (min_a_value_A > Convert.ToDouble(dataGridView1.Rows[i].Cells[0].Value) && a_was_check_A[i] == false)
                    {
                        min_a_value_A = Convert.ToDouble(dataGridView1.Rows[i].Cells[0].Value); ////
                        cheking_a_index_A = i;

                    }
                }
                min_max_a_A[check_a_count_A] = cheking_a_index_A;
                a_was_check_A[cheking_a_index_A] = true;
                min_a_value_A = 1000000;
                check_a_count_A++;
            }

            if (Convert.ToDouble(dataGridView1.Rows[0].Cells[0].Value) == Convert.ToDouble(dataGridView1.Rows[1].Cells[0].Value) || Convert.ToDouble(dataGridView1.Rows[1].Cells[0].Value) == Convert.ToDouble(dataGridView1.Rows[2].Cells[0].Value) || Convert.ToDouble(dataGridView1.Rows[0].Cells[0].Value) == Convert.ToDouble(dataGridView1.Rows[2].Cells[0].Value))
            { rule_of_trap_A = false; }

            if (Convert.ToDouble(dataGridView1.Rows[min_max_a_A[0]].Cells[1].Value) >= Convert.ToDouble(dataGridView1.Rows[min_max_a_A[1]].Cells[1].Value) || Convert.ToDouble(dataGridView1.Rows[min_max_a_A[0]].Cells[2].Value) <= Convert.ToDouble(dataGridView1.Rows[min_max_a_A[1]].Cells[2].Value)) // здесь начинаем проверять чтобы границы удовлетворяли условиям трапеции
            { rule_of_trap_A = false; }
            if (Convert.ToDouble(dataGridView1.Rows[min_max_a_A[1]].Cells[1].Value) >= Convert.ToDouble(dataGridView1.Rows[min_max_a_A[2]].Cells[1].Value) || Convert.ToDouble(dataGridView1.Rows[min_max_a_A[1]].Cells[2].Value) <= Convert.ToDouble(dataGridView1.Rows[min_max_a_A[2]].Cells[2].Value)) // здесь начинаем проверять чтобы границы удовлетворяли условиям трапеции
            { rule_of_trap_A = false; }

            bool rule_of_trap_B = true;
            int[] min_max_a_B = new int[4]; // переменные для отсортировки a lvl по возрастанию
            int check_a_count_B = 0;
            int cheking_a_index_B = 0;
            bool[] a_was_check_B = new bool[4] { false, false, false, false };
            double min_a_value_B = 1000000;

            while (check_a_count_B < 4)
            { // индексы эй выстраиваются по возрастанию
                for (int i = 0; i < 4; i++)
                {
                    if (min_a_value_B > Convert.ToDouble(dataGridView2.Rows[i].Cells[0].Value) && a_was_check_B[i] == false)
                    {
                        min_a_value_B = Convert.ToDouble(dataGridView2.Rows[i].Cells[0].Value); ////
                        cheking_a_index_B = i;

                    }
                }
                min_max_a_B[check_a_count_B] = cheking_a_index_B;
                a_was_check_B[cheking_a_index_B] = true;
                min_a_value_B = 1000000;
                check_a_count_B++;
            }
            if (Convert.ToDouble(dataGridView2.Rows[0].Cells[0].Value) == Convert.ToDouble(dataGridView2.Rows[1].Cells[0].Value) || Convert.ToDouble(dataGridView2.Rows[1].Cells[0].Value) == Convert.ToDouble(dataGridView2.Rows[2].Cells[0].Value) || Convert.ToDouble(dataGridView2.Rows[2].Cells[0].Value) == Convert.ToDouble(dataGridView2.Rows[3].Cells[0].Value) || Convert.ToDouble(dataGridView2.Rows[2].Cells[0].Value) == Convert.ToDouble(dataGridView2.Rows[0].Cells[0].Value) || Convert.ToDouble(dataGridView2.Rows[3].Cells[0].Value) == Convert.ToDouble(dataGridView2.Rows[0].Cells[0].Value) || Convert.ToDouble(dataGridView2.Rows[3].Cells[0].Value) == Convert.ToDouble(dataGridView2.Rows[1].Cells[0].Value))
            { rule_of_trap_B = false; }

            if (Convert.ToDouble(dataGridView2.Rows[min_max_a_B[0]].Cells[1].Value) >= Convert.ToDouble(dataGridView2.Rows[min_max_a_B[1]].Cells[1].Value) || Convert.ToDouble(dataGridView2.Rows[min_max_a_B[0]].Cells[2].Value) <= Convert.ToDouble(dataGridView2.Rows[min_max_a_B[1]].Cells[2].Value)) // здесь начинаем проверять чтобы границы удовлетворяли условиям трапеции
            { rule_of_trap_B = false; }
            if (Convert.ToDouble(dataGridView2.Rows[min_max_a_B[1]].Cells[1].Value) >= Convert.ToDouble(dataGridView2.Rows[min_max_a_B[2]].Cells[1].Value) || Convert.ToDouble(dataGridView2.Rows[min_max_a_B[1]].Cells[2].Value) <= Convert.ToDouble(dataGridView2.Rows[min_max_a_B[2]].Cells[2].Value)) // здесь начинаем проверять чтобы границы удовлетворяли условиям трапеции
            { rule_of_trap_B = false; }
            if (Convert.ToDouble(dataGridView2.Rows[min_max_a_B[2]].Cells[1].Value) >= Convert.ToDouble(dataGridView2.Rows[min_max_a_B[3]].Cells[1].Value) || Convert.ToDouble(dataGridView2.Rows[min_max_a_B[2]].Cells[2].Value) <= Convert.ToDouble(dataGridView2.Rows[min_max_a_B[3]].Cells[2].Value)) // здесь начинаем проверять чтобы границы удовлетворяли условиям трапеции
            { rule_of_trap_B = false; }

            if (!rule_of_trap_B)
            {
                MessageBox.Show("Некорректное заполнение таблицы B!");
                return;
            }

            if (!rule_of_trap_A)
            {
                MessageBox.Show("Некорректное заполнение таблицы A!");
                return;
            }
            // здесь уже вычисления
            // double[] a_lvls = new double[3];
            //int[] number_of_lvl = new int[3]; 
            //for (int i = 0; i < 3; i++) { // здесь мы передаём альфу которая посерёдке между 0 и 1
            //  if ( (Convert.ToDouble(dataGridView1.Rows[i].Cells[0].Value) != 0) && (Convert.ToDouble(dataGridView1.Rows[i].Cells[0].Value) != 1)){
            //    a_lvls[0] = Convert.ToDouble(dataGridView1.Rows[i].Cells[0].Value);
            //  number_of_lvl[0] = 1;
            // }
            // }
            double[] values_a_A = new double[3];
            double[] values_a_B = new double[4];
            int[] index_of_a_A = new int[3];
            int[] index_of_a_B = new int[4];

            //for (int i = 0; i < 3; i++) { 
            
            //}
        }

        private void button9_Click(object sender, EventArgs e)
        {

            bool correct_values = true;
            bool rule_of_trap_A = true;
            bool have_one_A = false;
            bool have_null_A = false;
            bool have_one_B = false;
            bool have_null_B = false;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (!double.TryParse(Convert.ToString(dataGridView1.Rows[i].Cells[j].Value), out double number))
                        correct_values = false;
                }
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (!double.TryParse(Convert.ToString(dataGridView2.Rows[i].Cells[j].Value), out double number))
                        correct_values = false;
                }
            }

            for (int i = 0; i < 3; i++)
            {
                if (Convert.ToDouble(dataGridView1.Rows[i].Cells[0].Value) == 1)
                    have_one_A = true;
                if (Convert.ToDouble(dataGridView1.Rows[i].Cells[0].Value) == 0)
                    have_null_A = true;
            }

            for (int i = 0; i < 4; i++)
            {
                if (Convert.ToDouble(dataGridView2.Rows[i].Cells[0].Value) == 1)
                    have_one_B = true;
                if (Convert.ToDouble(dataGridView2.Rows[i].Cells[0].Value) == 0)
                    have_null_B = true;
            }

            if (!have_one_B || !have_null_B)
            {
                correct_values = false;
            }

            if (!have_one_A || !have_null_A)
            {
                correct_values = false;
            }

            if (!correct_values)
            {
                MessageBox.Show("Некорректные данные!");
                return;
            }

            int[] min_max_a_A = new int[3]; // переменные для отсортировки a lvl по возрастанию
            int check_a_count_A = 0;
            int cheking_a_index_A = 0;
            bool[] a_was_check_A = new bool[3] { false, false, false };
            double min_a_value_A = 1000000;

            while (check_a_count_A < 3)
            { // индексы эй выстраиваются по возрастанию
                for (int i = 0; i < 3; i++)
                {
                    if (min_a_value_A > Convert.ToDouble(dataGridView1.Rows[i].Cells[0].Value) && a_was_check_A[i] == false)
                    {
                        min_a_value_A = Convert.ToDouble(dataGridView1.Rows[i].Cells[0].Value); ////
                        cheking_a_index_A = i;

                    }
                }
                min_max_a_A[check_a_count_A] = cheking_a_index_A;
                a_was_check_A[cheking_a_index_A] = true;
                min_a_value_A = 1000000;
                check_a_count_A++;
            }

            if (Convert.ToDouble(dataGridView1.Rows[0].Cells[0].Value) == Convert.ToDouble(dataGridView1.Rows[1].Cells[0].Value) || Convert.ToDouble(dataGridView1.Rows[1].Cells[0].Value) == Convert.ToDouble(dataGridView1.Rows[2].Cells[0].Value) || Convert.ToDouble(dataGridView1.Rows[0].Cells[0].Value) == Convert.ToDouble(dataGridView1.Rows[2].Cells[0].Value))
            { rule_of_trap_A = false; }

            if (Convert.ToDouble(dataGridView1.Rows[min_max_a_A[0]].Cells[1].Value) >= Convert.ToDouble(dataGridView1.Rows[min_max_a_A[1]].Cells[1].Value) || Convert.ToDouble(dataGridView1.Rows[min_max_a_A[0]].Cells[2].Value) <= Convert.ToDouble(dataGridView1.Rows[min_max_a_A[1]].Cells[2].Value)) // здесь начинаем проверять чтобы границы удовлетворяли условиям трапеции
            { rule_of_trap_A = false; }
            if (Convert.ToDouble(dataGridView1.Rows[min_max_a_A[1]].Cells[1].Value) >= Convert.ToDouble(dataGridView1.Rows[min_max_a_A[2]].Cells[1].Value) || Convert.ToDouble(dataGridView1.Rows[min_max_a_A[1]].Cells[2].Value) <= Convert.ToDouble(dataGridView1.Rows[min_max_a_A[2]].Cells[2].Value)) // здесь начинаем проверять чтобы границы удовлетворяли условиям трапеции
            { rule_of_trap_A = false; }

            bool rule_of_trap_B = true;
            int[] min_max_a_B = new int[4]; // переменные для отсортировки a lvl по возрастанию
            int check_a_count_B = 0;
            int cheking_a_index_B = 0;
            bool[] a_was_check_B = new bool[4] { false, false, false, false };
            double min_a_value_B = 1000000;

            while (check_a_count_B < 4)
            { // индексы эй выстраиваются по возрастанию
                for (int i = 0; i < 4; i++)
                {
                    if (min_a_value_B > Convert.ToDouble(dataGridView2.Rows[i].Cells[0].Value) && a_was_check_B[i] == false)
                    {
                        min_a_value_B = Convert.ToDouble(dataGridView2.Rows[i].Cells[0].Value); ////
                        cheking_a_index_B = i;

                    }
                }
                min_max_a_B[check_a_count_B] = cheking_a_index_B;
                a_was_check_B[cheking_a_index_B] = true;
                min_a_value_B = 1000000;
                check_a_count_B++;
            }
            if (Convert.ToDouble(dataGridView2.Rows[0].Cells[0].Value) == Convert.ToDouble(dataGridView2.Rows[1].Cells[0].Value) || Convert.ToDouble(dataGridView2.Rows[1].Cells[0].Value) == Convert.ToDouble(dataGridView2.Rows[2].Cells[0].Value) || Convert.ToDouble(dataGridView2.Rows[2].Cells[0].Value) == Convert.ToDouble(dataGridView2.Rows[3].Cells[0].Value) || Convert.ToDouble(dataGridView2.Rows[2].Cells[0].Value) == Convert.ToDouble(dataGridView2.Rows[0].Cells[0].Value) || Convert.ToDouble(dataGridView2.Rows[3].Cells[0].Value) == Convert.ToDouble(dataGridView2.Rows[0].Cells[0].Value) || Convert.ToDouble(dataGridView2.Rows[3].Cells[0].Value) == Convert.ToDouble(dataGridView2.Rows[1].Cells[0].Value))
            { rule_of_trap_B = false; }

            if (Convert.ToDouble(dataGridView2.Rows[min_max_a_B[0]].Cells[1].Value) >= Convert.ToDouble(dataGridView2.Rows[min_max_a_B[1]].Cells[1].Value) || Convert.ToDouble(dataGridView2.Rows[min_max_a_B[0]].Cells[2].Value) <= Convert.ToDouble(dataGridView2.Rows[min_max_a_B[1]].Cells[2].Value)) // здесь начинаем проверять чтобы границы удовлетворяли условиям трапеции
            { rule_of_trap_B = false; }
            if (Convert.ToDouble(dataGridView2.Rows[min_max_a_B[1]].Cells[1].Value) >= Convert.ToDouble(dataGridView2.Rows[min_max_a_B[2]].Cells[1].Value) || Convert.ToDouble(dataGridView2.Rows[min_max_a_B[1]].Cells[2].Value) <= Convert.ToDouble(dataGridView2.Rows[min_max_a_B[2]].Cells[2].Value)) // здесь начинаем проверять чтобы границы удовлетворяли условиям трапеции
            { rule_of_trap_B = false; }
            if (Convert.ToDouble(dataGridView2.Rows[min_max_a_B[2]].Cells[1].Value) >= Convert.ToDouble(dataGridView2.Rows[min_max_a_B[3]].Cells[1].Value) || Convert.ToDouble(dataGridView2.Rows[min_max_a_B[2]].Cells[2].Value) <= Convert.ToDouble(dataGridView2.Rows[min_max_a_B[3]].Cells[2].Value)) // здесь начинаем проверять чтобы границы удовлетворяли условиям трапеции
            { rule_of_trap_B = false; }

            if (!rule_of_trap_B)
            {
                MessageBox.Show("Некорректное заполнение таблицы B!");
                return;
            }
            
            if (!rule_of_trap_A) {
                MessageBox.Show("Некорректное заполнение таблицы A!");
                return;
            }

           

            

                double sum_a = 0, sum_b = 0;
                double A, B;
                for (int i = 0; i < 3; i++)
                {

                    for (int j = 1; j < 3; j++)
                    {
                        sum_a += Convert.ToDouble(dataGridView1.Rows[i].Cells[j].Value);
                        sum_b += Convert.ToDouble(dataGridView2.Rows[i].Cells[j].Value);
                    }
                }

                A = sum_a / 3;
                B = sum_b / 4;

                if (A == B)
                {
                    richTextBox1.Text = "A = B";
                }

                else if (A > B)
                {
                    richTextBox1.Text = "A > B";
                }
                else
                {
                    richTextBox1.Text = "A < B";
                }

            
        }
    }
}
