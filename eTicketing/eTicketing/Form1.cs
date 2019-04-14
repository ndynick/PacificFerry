using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;

namespace eTicketing
{
    public partial class PacificCruise : Form
    {
        public PacificCruise()
        {
            InitializeComponent();
        }

        int row=0;
        int seat = 0;
        int quantity = 1;
        float price = 0f;
        float grandtotal = 0f;
        string type = "";
        string selectedDest = "";
        string selectedtype = "";
        string manager = "Nick Low";
        string[,] rowSystem = new string[10, 8];
        string[,] rowLetter = new string[10, 8];
        int[,] seatNum = new int[10, 8];
        string[,] cust = new string[10, 8];
        string[,] nric = new string[10, 8];
        string[,] nationality = new string[10, 8];
        string[,] sex = new string[10, 8];
        string[,] address = new string[10, 8];
        int[,] contactno = new int[10, 8];
        string[,] email = new string[10, 8];
        ArrayList CustAL = new ArrayList();

        //Using Queue
        Queue myQueue = new Queue();

        private void btnBooking_Click(object sender, EventArgs e)
        {
            char rowc;
            int contactnum=0;
            string name = txtCustName.Text;
            string selectedgender = "";
            quantity = int.Parse(txtQuantity.Text);
            bool row_Valid = false;
            bool seat_Valid = false;
            bool custname_Check=txtCustName.Text.Contains('0') || txtCustName.Text.Contains('1') || txtCustName.Text.Contains('2') ||txtCustName.Text.Contains('3') ||txtCustName.Text.Contains('4') ||txtCustName.Text.Contains('5') ||txtCustName.Text.Contains('6') || txtCustName.Text.Contains('7') ||txtCustName.Text.Contains('8') ||txtCustName.Text.Contains('9');
            bool custname_valid = false;
            bool nationality_Check = txtNationality.Text.Contains('0') || txtNationality.Text.Contains('1') || txtNationality.Text.Contains('2') || txtNationality.Text.Contains('3') || txtNationality.Text.Contains('4') || txtNationality.Text.Contains('5') || txtNationality.Text.Contains('6') || txtNationality.Text.Contains('7') || txtNationality.Text.Contains('8') || txtNationality.Text.Contains('9');
            bool nationality_valid = false;
            bool contact_valid = false;
            bool email_Check1= txtEmail.Text.Contains("@"); 
            bool email_Check2=txtEmail.Text.Contains(" ");
            bool email_Check3=txtEmail.Text.Contains(".com");
            bool email_Valid = false;
            bool destination = false;
            destination = (rdoBintan.Checked || rdoBatam.Checked || rdoChild.Checked || rdoAdult.Checked);

            if (destination == true)
            {
                if ((rdoBintan.Checked == true) && (rdoChild.Checked == true))
                {
                    selectedDest = "Bintan";
                    type = "Child";
                    price = (quantity * 35f) + (quantity * 1.2f);
                }
                else if ((rdoBintan.Checked == true) && (rdoAdult.Checked == true))
                {
                    selectedDest = "Bintan";
                    type = "Adult";
                    price = (quantity * 70f) + (quantity * 1.2f);
                }

                else if ((rdoBatam.Checked == true) && (rdoChild.Checked == true))
                {
                    selectedDest = "Batam";
                    type = "Child";
                    price = (quantity * 30f) + (quantity * 1.2f);

                }
                else if ((rdoBatam.Checked == true) && (rdoAdult.Checked == true))
                {
                    selectedDest = "Batam";
                    type = "Adult";
                    price = (quantity * 60f) + (quantity * 1.2f);
                }
                grandtotal = grandtotal + price;
                
            }
            else
            {
                MessageBox.Show("Please select your destination and fare type!", "Error");
               
            }
                if (char.TryParse(txtRow.Text, out rowc) == false)
                {
                    MessageBox.Show("Please enter in your row in alphabetical", "Error");
                    txtRow.Clear();
                    txtRow.Focus();
                }
                else if ((rowc < 'a') || (rowc > 'j'))
                {
                    MessageBox.Show("Please enter from A to J", "Error");
                    txtRow.Clear();
                    txtRow.Focus();
                }
                else
                {
                    row_Valid = true;
                    row = rowc - 'a';
                     //convert row a to o, b to 1....
                }
            
                if (int.TryParse(txtSeat.Text, out seat) == false)
                {
                    MessageBox.Show("Please enter in your seat in numerical", "Error");
                    txtSeat.Clear();
                    txtSeat.Focus(); 
                }
                else if ((seat < 0) || (seat > 7))
                {
                    MessageBox.Show("Please enter from 1 to 8", "Error");
                    txtSeat.Clear();
                    txtSeat.Focus();
                }
                else
                {
                    seat_Valid = true; 
                }

                if (txtCustName.TextLength == 0)
                {
                    MessageBox.Show("Please enter your name", "Error");
                }
                else if(custname_Check==true)
                {
                    MessageBox.Show("Please enter your name in alphabetical","Error");
                }
                else
                {
                    custname_valid = true;
                }

                if (txtNric.TextLength == 0)
                {
                    MessageBox.Show("Please enter your NRIC", "Error");
                }
                else if(nationality_Check==true)
                {
                    MessageBox.Show("Please enter your nationality in alphabetical","Error");   
                }
                else
                {
                    nationality_valid = true;
                }


            //CONTACT VALIDATION IS DONE
             if (txtContactNo.TextLength == 0)
                    {
                        MessageBox.Show("Please enter your contact number", "Error");
                    }
             else if (int.TryParse(txtContactNo.Text, out contactnum) == false)
             {
                 MessageBox.Show("Please enter your in contact number in numerical! ", "Error");
             }
             else
             {
                 contact_valid = true;
             }

            //EMAIL VALIDATION IS DONE (Check @, SPACE, .com)
            if (txtEmail.TextLength == 0)
                    {
                        MessageBox.Show("Please enter your email ", "Error");
                        txtEmail.Clear();
                        txtEmail.Focus();
                    }
            else if(email_Check1==false)
            {
                MessageBox.Show("Please enter a valid email with @ ","Error");
                txtEmail.Clear();
                txtEmail.Focus();
            }
            else if (email_Check2==true)
            {
                MessageBox.Show("Please enter a valid email without SPACE in between ", "Error");
                txtEmail.Clear();
                txtEmail.Focus();
            }
            else if (email_Check3 == false)
            {
                MessageBox.Show("Please enter a valid email with .com ", "Error");
                txtEmail.Clear();
                txtEmail.Focus();
            }
            else
            {
                email_Valid = true;
            }
            
            if (rdoMale.Checked == true)
            {
                selectedgender = "Male";
            }
            else
            {
                selectedgender = "Female";
            }
            

            if (row_Valid == true && seat_Valid == true  && custname_valid==true && nationality_valid==true && contact_valid==true && email_Valid==true)
            {
                CustAL.Add(name);

                if (rowSystem[row, seat] == null) //is not occupied
                {
                    if (txtNationality.TextLength == 0)
                    {
                        MessageBox.Show("Please enter your nationlity", "Error");
                    }
                    else if (rdoMale.Checked == false && rdoFemale.Checked == false)
                    {
                        MessageBox.Show("Please choose your gender", "Error");
                        
                    }
                    else if (txtAddress.TextLength == 0)
                    {
                        MessageBox.Show("Please enter your address", "Error");
                    }
                    else if (rdoBintan.Checked == false && rdoBatam.Checked == false)
                    {
                        MessageBox.Show("Please choose your destination", "Error");
                    }
                    else if (rdoChild.Checked == false && rdoAdult.Checked == false)
                    {
                        MessageBox.Show("Please choose your fare type", "Error");
                    }
                    else
                    {
                        cust[row, seat] = txtCustName.Text;
                        nric[row, seat] = txtNric.Text;
                        nationality[row, seat] = txtNationality.Text;
                        sex[row, seat] = selectedgender;
                        address[row, seat] = txtAddress.Text;
                        contactno[row, seat] = contactnum;
                        email[row, seat] = txtEmail.Text;
                        rowLetter[row, seat] = txtRow.Text;
                        seatNum[row, seat] = int.Parse(txtSeat.Text);
                        MessageBox.Show("1 booking added\n");
                        rtbBooking.AppendText(rowLetter[row,seat] + seatNum[row,seat] + "                " + quantity + "                 " + type + "          " + selectedDest + "              $" + price.ToString() + "\n");
                    }
                }
                else
                {
                    MessageBox.Show("It is occupied");
                }
            }
        }

        private void btnTotalRowSeat_Click(object sender, EventArgs e)
        {
            int counter = 0;

            for (int row = 0; row < rowSystem.GetLength(0); row++)
            {
                for (int seat = 0; seat < rowSystem.GetLength(1); seat++)
                {
                    if (cust[row, seat] != null)
                    {
                        counter++;  
                    }
                }
            }
            MessageBox.Show("The total number of booking in all row and seat is " + counter);
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            int cvcno = 0;
            int cardno = 0;
            lblTotal.Text = "$" + grandtotal.ToString();
            bool payment = false;
            payment = (rdoCash.Checked || rdoAE.Checked || rdoMC.Checked || rdoVisa.Checked);
            bool cvc_valid = false;
            bool cardno_valid = false;

            if (payment == true)
            {
                if (rdoCash.Checked == true)
                {
                    selectedtype = "Cash";

                }
                else if (rdoAE.Checked == true)
                {
                    selectedtype = "American Express";

                }
                else if (rdoMC.Checked == true)
                {
                    selectedtype = "Mastercard";

                }
                else if (rdoVisa.Checked == true)
                {
                    selectedtype = "Visa";

                }

            }
            else
            {
                MessageBox.Show("Please select your payment type! ", "Error");
                rdoCash.Focus();
            }

           
            if (txtCardNo.TextLength == 0)
            {
                MessageBox.Show("Please enter your 16 digits card number! ", "Error");
                txtCardNo.Clear();
                txtCardNo.Focus();
            }
            else
            {
                cardno_valid = true;
            }

            if (txtCvc.TextLength == 0)
            {
                MessageBox.Show("Please enter your 3 digits CVC number! ", "Error");
                txtCvc.Clear();
                txtCvc.Focus();
            }
            else if (int.TryParse(txtCvc.Text, out cvcno) == false)
            {
                MessageBox.Show("Please enter your CVC number in numerical! ", "Error");
                txtCvc.Clear();
                txtCvc.Focus();
            }

            else
            {
                cvc_valid = true;
            }

            if (payment == true && cardno_valid == true && cvc_valid == true)
            {
                for (int row = 0; row < rowSystem.GetLength(0); row++)
                {
                    string display;

                    for (int seat = 0; seat < rowSystem.GetLength(1); seat++)
                    {
                        if (cust[row, seat] != null)
                        {
                            display = "=============================================================================================================================================" + "\n";
                            display = display + "Date: " + dateTimePicker1.Text + " | " + "Time: " + dateTimePicker2.Text + "\n" + "\n";
                            display = display + ".:: Customer Information ::." + "\n";
                            display = display + "Customer Name: " + cust[row, seat] + "\n" + "NRIC: " + nric[row, seat] + "\n" + "Nationality: " + nationality[row, seat] + "\n";
                            display = display + "Sex: " + sex[row, seat] + "\n" + "Address: " + address[row, seat] + "\n";
                            display = display + "Contact Number: " + contactno[row, seat] + "\n" + "Email: " + email[row, seat] + "\n";
                            display = display + "Mode Of Payment: " + selectedtype + "\n" + "Card Number: " + cardno + "\n";
                            display = display + "CVC Number: " + cvcno + "\n" + "Card Expiry Date: " + dateTimePicker3.Text + "\n";
                            display = display + "Total Amount is $: " + grandtotal + "\n";
                            display = display + "\n" + "\n" + ".:: Customer's Booking Information ::." + "\n";
                            display = display + "Seat Number: " + rowLetter[row,seat] + seatNum[row,seat] + "\n" + "Quantity: " + quantity + "\n";
                            display = display + "Type: " + type + "\n" + "Destination: " + selectedDest + "\n";
                            display = display + "Manager Name: " + manager + "\n";
                            display = display + "=============================================================================================================================================" + "\n";
                            rtbDisplay.AppendText(display);
                        }

                    }
                }
            }
            
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            rdoBintan.Checked = false;
            rdoBatam.Checked = false;
            rdoChild.Checked = false;
            rdoAdult.Checked = false;
            txtRow.Clear();
            txtSeat.Clear();
        }

        private void btnSeatAvail_Click(object sender, EventArgs e)
        {
            int counter = 0;
            int leftcount = 0;
            for (int row = 0; row < rowSystem.GetLength(0); row++)
            {

                for (int seat = 0; seat < rowSystem.GetLength(1); seat++)
                {
                    if (cust[row, seat] != null)
                    {
                        counter++;
                        leftcount = 80 - counter;
                    }
                }

            }
            MessageBox.Show(leftcount + " tickets is available!");
        }

        private void btnCheckSeatDetail_Click(object sender, EventArgs e)
        {
            for (int r = 0; r < rowSystem.GetLength(0); r++)
            {
                for (int s = 0; s < rowSystem.GetLength(1); s++)
                {
                    if (cust[r, s] != null)
                    {
                        rtbCheckBooking.AppendText(rowLetter[row,seat] + seatNum[row,seat] +"                " + quantity + "                 " + type + "          " + selectedDest + "              $" + price.ToString() + "\n");

                    }
                }

            }
        }
            
        private void btnModify_Click(object sender, EventArgs e)
        {
            manager = txtManager.Text;
            MessageBox.Show("The username is updated!","Update Message");
            lblServedby.Text = manager; 
        }

        //---------------------------------------------------------
        //CODES FOR LEFT & RIGHT BUTTON IS DONE
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage1;
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage3;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage1;
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage4;
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage5;
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage4;
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage6;
        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage5;
        }

        //--------------------------------------------------------

        private void btnServeQ_Click(object sender, EventArgs e)
        {
            string servingName;
            if (myQueue.Count != 0)
            {
                servingName = (string)myQueue.Dequeue();
                rtbQueueDisplay.AppendText("It is serving " + servingName + "\n");
            }
            else
            {
                rtbQueueDisplay.AppendText("Queue is emptied. \n");
            }
        }

        private void btnAddQ_Click(object sender, EventArgs e)
        {
            string addQ = txtQueue.Text;
            myQueue.Enqueue(addQ);
            rtbQueueDisplay.AppendText("Adding " + addQ + " into queue \n");
        }

        private void btnPrintQ_Click(object sender, EventArgs e)
        {
            rtbQueueDisplay.AppendText("Items in queue are: ");
            foreach (string item in myQueue)
            {
                rtbQueueDisplay.AppendText(item + " ");
            }
        }

        private void btnClearQ_Click(object sender, EventArgs e)
        {
            myQueue.Clear();
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            StreamWriter wr = new StreamWriter("CustomerRecord.txt", true);
            string custrecord = rtbDisplay.Text;
            wr.WriteLine(custrecord);
            wr.Close();
            MessageBox.Show("Customer's record is updated!");
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            StreamReader sr = new StreamReader("CustomerRecord.txt");
            string custrecord;
            custrecord = sr.ReadLine();
            while (custrecord != null)
            {
                rtbDisplay.AppendText(custrecord + "\n");
                custrecord = sr.ReadLine();
            }
            sr.Close();
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            rtbSort.Clear();
            string name = txtCustName.Text;
            
            for (int row = 0; row < cust.GetLength(0); row++)
            {
                for (int seat = 0; seat < cust.GetLength(1); seat++)
                {
                    cust[row, seat] = name;                    
                    CustAL.Sort();
                }
            }
            rtbSort.AppendText("Customer names sorted\n");
            rtbSort.AppendText("Items in array list are: ");

            foreach (string item in CustAL) 
            {
                rtbSort.AppendText(item + " " );
            }
            rtbSort.AppendText("\n");
        } 
    }
}      
      

       

        
    
