using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using Pickering.Lxi.Piplx;

namespace Pickering_Submission
{
    public partial class Form1 : Form
    {
        public static PiplxManager manager = new PiplxManager();
        public static System.Timers.Timer timer;

        public Form1()
        {
            //Initialise form and load saved variables
            InitializeComponent();

            IPBox.Text = Properties.Settings.Default.SavedIP;
            UpdtFrq.Text = $"{Properties.Settings.Default.TimeIncrement}";


            timer = new System.Timers.Timer(Properties.Settings.Default.TimeIncrement); // Interval in milliseconds (1000ms = 1 second)
            timer.Elapsed += UpdateVariable; 
            timer.AutoReset = true; 
            timer.Enabled = true;
            
        }
        private void UpdateVariable(object sender, ElapsedEventArgs e)
        {
            UpdateDetailBox();

        }
 

        private void CardSelector_SelectedIndexChanged(object sender=null, EventArgs e=null)
        {
            //Updates card details when new card is selected
            UpdateDetailBox();
        }
        
        private int GetCurrentCard()
        {
            int index = 0;
            CardSelector.Invoke(new Action(() =>
            {
                index = CardSelector.SelectedIndex;
            }));
            return index;
        }

        private void UpdateDetailBox()
        {
            //Displays card details

            int index = GetCurrentCard();
            if (index == -1)
            {
                return;
            }
            
            string details = "";
            PiplxCardInfo info = (PiplxCardInfo)manager.Cards[index].Info;

            // details += info.AliasRealTypeCode;

            details += "\nType Code:   ";
            details += info.TypeCode;
            details += "\nType Info:   ";
            details += info.TypeInfo;
            details += "\nBus Location:   ";
            details += info.Bus.ToString();
            details += "\nDevice Location:   ";
            details += info.Device.ToString();
            details += "\nDiagnostic:   ";
            details += info.Diagnostic();
            details += "\nStatus Message:   ";
            details += info.GetStatusMessage();
            details += "\nCard ID:   ";
            //details += info.GlobalAddressSlotNumber.ToString();
            details += info.Id.ToString();
            details += "\nInput SubUnit No:   ";
            details += info.InputSubunitsCount.ToString();
            details += "\nOutput SubUnit No:   ";
            //details += info.IsAliasModeActive.ToString();
            details += info.OutputSubunitsCount.ToString();
            details += "\nRevision Code:   ";
            details += info.RevisionCode;
            details += "\nSerial Number:   ";
            details += info.SerialNumber;
            
            //CardStatuses status = info.Status();
            //details += info.TemperatureSensorCount.ToString();
            
           DetailBox.Invoke(new Action(() =>
           {
                DetailBox.Text = details;
           }));
            

        }


        public static void Printer(string header, string body)
        {
            //Prints error messages
            
            
            MessageBox.Show($"\n{header}:\n{body}", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
        }
          
        private void IPEnter_Click(object sender, EventArgs e)
        {
            //Enters / saves IP address from relevant text box
            string IP = IPBox.Text;
            
            
            try
            {
                manager.Connect(IP, 9999, 1024);
            }
            catch (Pickering.Lxi.Piplx.PiplxException ex)
            {
                Printer("Connection Error", $"{ex.Message}");
                return;
            }


            int cardnum = manager.CardsCount;
            if (cardnum == 0)
            {
                Printer("Error", "No cards detected");
                return;
            }


            CardSelector.Items.Clear();
            for (int i = 0; i < cardnum; i++)
            {
                PiplxCardInfo info = (PiplxCardInfo)manager.Cards[i].Info;
                CardSelector.Items.Add(info.TypeCode);

            }

            CreateButtons(cardnum);

            Properties.Settings.Default.SavedIP = IP;
            Properties.Settings.Default.Save();
        }

        

        private void CreateButtons(int buttonCount)
        {
            // Creats labelled on / off buttons for each card

            ButtonHolder.Controls.Clear();

                for (int i = 0; i < buttonCount; i++)
                {
                    PiplxCardInfo info = (PiplxCardInfo)manager.Cards[i].Info;
                    Button btn = new Button
                    {
                        Text = $"{i + 1}: {info.TypeCode}",
                        Width = 100,
                        Height = 50,
                        BackColor = Color.Red,
                        Name = $"CardButton{i}",
                        Tag = i
                    
                    };
                    btn.Click += Button_Click;
                    ButtonHolder.Controls.Add(btn);
               }
                ChangeAllButtonColours();

                

                
            }
        

        private void Button_Click(object sender, EventArgs e)
        {
            //Opens / closes cards

            if (sender is Button clickedButton)
            {
                
                string buttonText = clickedButton.Text;
                int buttonNumber = (int)clickedButton.Tag;
                int cardnum = manager.CardsCount;

                
                if (manager.Cards[buttonNumber].IsOpen())
                {
                    manager.Cards[buttonNumber].Close();
                }
                else
                {
                    manager.Cards[buttonNumber].Open();
                }

                ChangeAllButtonColours();
            }
        }

        private void ChangeAllButtonColours()
        {
            //Updates on/off button colours when they are loaded/ pressed

            foreach (Control control in ButtonHolder.Controls)
            {

                
                if (control is Button btn)
                {
                    int btnnbr = (int)btn.Tag;
                    if (manager.Cards[btnnbr].IsOpen())
                    {
                        btn.BackColor = Color.Green;
                    }
                    else
                    {
                        btn.BackColor = Color.Red;
                    }
                    
                }
            }
        }

        private void OpenCard_Click(object sender, EventArgs e)
        {
            //Opens card as new window

            int index = GetCurrentCard();
            if (index == -1)
            {
                return;
            }

            PiplxCard card = (PiplxCard)manager.Cards[index];

            
            
            if (!card.IsOpen())
            {
                Form1.Printer("Card needs to be open", "Check bottom of main window to open/ close cards.");
                return;
            }
            else
            {
                PopUp popup = new PopUp(card);
                popup.Show();
            }
            
        
        }

        private void UpdtFrqEnter(object sender, KeyEventArgs e)
        {
            //Changes update frequency on enter key being hit

            if (e.KeyCode == Keys.Enter)
            {
                
                if (!string.IsNullOrWhiteSpace(UpdtFrq.Text))
                {
                    
                    bool isInt = int.TryParse(UpdtFrq.Text, out int frequ);
                    if (isInt)
                    {
                        if (100 <= frequ && frequ <= 10000)
                        {
                        Properties.Settings.Default.TimeIncrement = frequ;
                        MessageBox.Show($"You have successfully changed the Update Frequency to {frequ}ms.");
                        e.SuppressKeyPress = true;
                        }
                        else
                        {
                            Printer("Out of bounds", "Please enter a value between 100 and 10000.");
                        }
                    }
                    else
                    {
                        Printer("Not an integer", "Please enter an integer.");
                    }

                }
                else
                {
                    Printer("Not an integer", "Please enter an integer.");
                }
            }
        }
        

        
    }

}

