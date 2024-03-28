using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace word
{
    public partial class ShowInfor : Form
    {
        public ShowInfor()
        {
            InitializeComponent();
            this.Text = "Information's Team";
        }
        private Dictionary<string, string[]> itemData;
        private void ShowInfor_Load(object sender, EventArgs e)
        {
            itemData = new Dictionary<string, string[]>();
            itemData.Add("2000003649", new string[] { "Tiến Phát", "2000003649", "20BITV02" , "phat13122002@gmail.com" });
            itemData.Add("2100005929", new string[] { "Nguyễn Tấn Nhã", "2100005929", "21BITV02", "tannha.nam2003@gmail.com" });
            itemData.Add("2000000740", new string[] { "Nguyễn Triệu Vỹ", "2000000740", "20BITV02" , "trieuvyxka1@gmail.com" });
            listView.View = View.List;
            foreach (string item in itemData.Keys)
            {
                listView.Items.Add(item);
            }

            listView.SelectedIndexChanged += ListView_SelectedIndexChanged;
        }
        private void ListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView.SelectedIndices.Count > 0)
            {
                int selectedIndex = listView.SelectedIndices[0];
                string selectedItem = listView.Items[selectedIndex].Text;

                if (itemData.ContainsKey(selectedItem))
                {
                    string[] itemInfo = itemData[selectedItem];
                    string message = $"Team: {itemInfo[0]}\nMSSV: {itemInfo[1]}\nClass: {itemInfo[2]}\nEmail: {itemInfo[3]} ";
                    MessageBox.Show(message, "Your Information");
                    
                }
            }
        }
    }
}
