using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Pickering.Lxi.Piplx;
using System.Text.RegularExpressions;

namespace Pickering_Submission
{
    public partial class PopUp : Form
    {
        //Declare fields belonging to Form
        public Dictionary<string, Label> labelsdict;
        public string subUnitType = "";
        public int subUnitNo = 0;

        public List<SwitchSubunit> subunitsSw = new List<SwitchSubunit>();
        public List<MultichannelMultiplexerSubunit> subunitsMUXM = new List<MultichannelMultiplexerSubunit>();
        public List<MatrixSubunit> subunitsMa = new List<MatrixSubunit>();

        public PiplxCard thecard;

        public FlowLayoutPanel VerticalPanel = new FlowLayoutPanel
        {
            FlowDirection = FlowDirection.TopDown,
            AutoSize = true,
            AutoScroll = true,
            WrapContents = false,
            Margin = new Padding(5),
            BackColor = Color.DarkGray,
            Visible = true,
            Dock = DockStyle.Fill
            // Add spacing between rows
        };

        public List<Label> details = new List<Label>{};

        public PopUp(PiplxCard card)
        {
            //Initialise variables
            InitializeComponent();
            thecard = card;
            PiplxCardInfo info = (PiplxCardInfo)thecard.Info;
            labelsdict = new Dictionary<string, Label>();


            Regex regswitch = new Regex(@"^40-131-\d{3}");
            Regex regmchanmplex = new Regex(@"^40-635A-\d{3}-M");
            Regex regmatrix = new Regex(@"^40-510-\d{3}");

            string TypeCode = info.TypeCode;
            int inputSubUnits = info.InputSubunitsCount;
            int outputSubUnits = info.OutputSubunitsCount;
            subUnitNo = outputSubUnits;
            Text = $"{TypeCode} Card";

            this.Controls.Add(VerticalPanel);
            VerticalPanel.Controls.Clear();

            int rows = 0;
            int cols = 0;

            if (inputSubUnits > 0){
                Form1.Printer("Error Reading Input Subunit","");
                Close();

            }
            
            
            //Displays info for subunits of relevant card
            if (regswitch.IsMatch(TypeCode))
            {
                subUnitType = "Switch";
                for (int i = 0; i < outputSubUnits; i++)
                {
                    subunitsSw.Add((SwitchSubunit)card.OutputSubunits[i]);
                    for (int j = 0; j < subunitsSw[i].BitsCount; j++)
                    {
                        subunitsSw[i].OperateBit(j+1, false);
                    }
                }

                for (int i = 0; i < outputSubUnits; i++)
                {
                    cols = subunitsSw[i].BitsCount;
                    rows = 1;
                    CreateRows(i, rows, cols);
                }
                
            }
            //~~~~####~~~~####~~~~####~~~~####~~~~####
            else if (regmchanmplex.IsMatch(TypeCode))
            {
                subUnitType = "MchanMplex";
                for (int i = 0; i < outputSubUnits; i++)
                {
                    subunitsMUXM.Add((MultichannelMultiplexerSubunit)card.OutputSubunits[i]);
                    for (int j = 0; j < subunitsMUXM[i].BitsCount; j++)
                    {
                        subunitsMUXM[i].OperateBit(j+1, false);
                    }
                }
                for (int i = 0; i < outputSubUnits; i++)
                {
                    cols = subunitsMUXM[i].BitsCount;
                    rows = 1;
                    CreateRows(i, rows, cols);
                }

                
            }

            //~~~~####~~~~####~~~~####~~~~####~~~~####
            else if (regmatrix.IsMatch(TypeCode))
            {
                subUnitType = "Matrix";
                for (int i = 0; i < outputSubUnits; i++)
                {
                    subunitsMa.Add((MatrixSubunit)card.OutputSubunits[i]);
                    for (int j = 0; j < subunitsMa[i].BitsCount; j++)
                    {
                        subunitsMa[i].OperateBit(j + 1, false);
                    }
                }

                for (int i = 0; i < outputSubUnits; i++)
                {   
                    cols = subunitsMa[i].Columns;
                    rows = subunitsMa[i].Rows;
                    CreateRows(i, rows, cols);   
                }
                
                
            }
            else
            {
                Form1.Printer("SubUnit type not supported.", "Please try using the Switch, Matrix and MultichannelMultiplexer subunits only.");
                Close();
            }



        }

        private string UpdateDetails(int i)
        {
            //Updates subunit details
            //i = subunit index



            SubunitType SUT;
            int count;
            int settletime;
            if (subUnitType == "Switch")
            {
                count = subunitsSw[i].BitsCount;
                settletime = subunitsSw[i].SettleTime;
                SUT = subunitsSw[i].SubunitType;
            }
            else if (subUnitType == "MchanMplex")
            {
                count = subunitsMUXM[i].BitsCount;
                settletime = subunitsMUXM[i].SettleTime;
                SUT = subunitsMUXM[i].SubunitType;
            }
            else if (subUnitType == "Matrix")
            {
                count = subunitsMa[i].BitsCount;
                settletime = subunitsMa[i].SettleTime;
                SUT = subunitsMa[i].SubunitType;
            }
            else
            {
                return "";
            }


            return $"Type:    {SUT}\n" +
                    $"Bit Count:    {count}\n" +
                    $"Settle Time:    {settletime}";
                
            
        }

       
        
        private void CreateRows(int subunit, int rowNumber, int buttonCount)
        {
            /*Creates button layout
            Buttons are contained in a row, and rows are contained in a column*/

            //Label to describe subunit details
            Label label = new Label
            {
                Margin = new Padding(20),
                AutoSize = true,
                BackColor = Color.LightGray,
                Text = $"SubUnit: {subunit}\n" +
                UpdateDetails(subunit)
            };
            VerticalPanel.Controls.Add(label);
            labelsdict[$"{subunit}"] = label;


            for (int j = 0; j < rowNumber; j++)
            {

                FlowLayoutPanel rowPanel = new FlowLayoutPanel
                {
                    FlowDirection = FlowDirection.LeftToRight,
                    AutoSize = true,
                    BackColor = Color.LightGray,
                    WrapContents = false,                      
                    Margin = new Padding(5),
                    Visible = true
                };

                for (int i = 0; i < buttonCount; i++)
                {
                    List<int> tag = new List<int> {subunit, j, i };
                    string text = $"({j},{i})";
                    Button btn = CreateButton(text, tag);
                    rowPanel.Controls.Add(btn);
                }

                VerticalPanel.Controls.Add(rowPanel);
            }
        }
        
        private Button CreateButton(string text, List<int> tag)
        {
            //Simplifies code in CreateRows
            Button btn = new Button
            {
                Text = text,
                Width = 50,
                Height = 20,
                BackColor = Color.Red,
                Name = text,
                Tag = tag
            };

                btn.Click += Button_Click;
            return btn;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            //Registers one of the buttons being pressed
            //Simulates bit switch and updates button colour + subunit details

            if (sender is Button clickedButton && clickedButton.Tag is List<int> tag)
            {
                bool newstate;
                if (clickedButton.BackColor == Color.Red)
                {
                    newstate = true;
                }
                else if (clickedButton.BackColor == Color.Green)
                {
                    newstate = false;
                }
                else
                {
                    return;
                }
                //subunitsSw
                //subunitsMUXM
                //subunitsMa
                PiplxCardInfo info = (PiplxCardInfo)thecard.Info;

                int subUnit = tag[0];
                int rowNumber = tag[1];  
                int colNumber = tag[2];
                

                if (!thecard.IsOpen())
                    {
                        Form1.Printer("Card needs to be open", "Check bottom of main window to open/ close cards.");
                        return;
                    }
                


                if (subUnitType == "Switch")
                {
                    
                   
                    int bit = colNumber;
                    subunitsSw[subUnit].OperateBit(bit + 1, newstate);
                    

                }
                else if (subUnitType == "MchanMplex")
                {
                   
                    int bit = colNumber;
                    subunitsMUXM[subUnit].OperateBit(bit + 1, newstate);
                    
                    
                }
                else if (subUnitType == "Matrix")
                {
                    int width = subunitsMa[subUnit].Columns;
                    int bit = (width * rowNumber) + colNumber;
                    subunitsMa[subUnit].OperateBit(bit + 1, newstate);

                }
                else
                {
                    Form1.Printer("Invalid Card Type", "Please try one of the supported card types");
                    return;
                }


                Color paint;
                if (newstate)
                {
                    paint = Color.Green;
                }
                else
                {
                    paint = Color.Red;
                }
                clickedButton.BackColor = paint;

                string key = $"{subUnit}";
                if (labelsdict.ContainsKey(key))
                {
                    labelsdict[key].Text = $"SubUnit: {subUnit}\n" +
                UpdateDetails(subUnit);
                }
            }
        }
    }
}
