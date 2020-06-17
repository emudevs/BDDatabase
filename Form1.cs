// Decompiled with JetBrains decompiler
// Type: BDDatabase.Form1
// Assembly: BDDatabase, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 13430B3E-0681-4043-8C46-F9A424DA21B6
// Assembly location: C:\Users\Admin\Music\Новая папка (4)\BDDatabase\BDDatabase.exe

using BDDatabase.Helper;
using BDDatabase.Properties;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace BDDatabase
{
  public class Form1 : Form
  {
    private List<Item> ItemList = new List<Item>();
    private Timer timer = new Timer();
    private IContainer components = (IContainer) null;
    private TextBox txtSearch;
    private ListView listItems;
    private Label lblSearch;
    private Label label1;
    private GroupBox grpInfo;
    private TextBox txtName;
    private PictureBox picItem;
    private Label lblId;
    private TextBox txtId;
    private Label lblName;
    private Panel pnlGrade;
    private ComboBox cmbLanguage;
    private Label lblLanguage;

    public Form1()
    {
      this.InitializeComponent();
      this.timer.Interval = 2000;
      this.timer.Tick += new EventHandler(this.Timer_Tick);
      this.cmbLanguage.SelectedIndex = 0;
      this.listItems.LargeImageList = new ImageList()
      {
        ImageSize = new Size(50, 50)
      };
      this.ChangeLanguage(0);
    }

    public void ChangeLanguage(int index)
    {
      switch (index)
      {
        case 1:
          this.ItemList = (List<Item>) JsonConvert.DeserializeObject<List<Item>>(Resources.items_de);
          break;
        case 2:
          this.ItemList = (List<Item>) JsonConvert.DeserializeObject<List<Item>>(Resources.items_fr);
          break;
        case 3:
          this.ItemList = (List<Item>) JsonConvert.DeserializeObject<List<Item>>(Resources.items_ru);
          break;
        case 4:
          this.ItemList = (List<Item>) JsonConvert.DeserializeObject<List<Item>>(Resources.items_es);
          break;
        case 5:
          this.ItemList = (List<Item>) JsonConvert.DeserializeObject<List<Item>>(Resources.items_sp);
          break;
        case 6:
          this.ItemList = (List<Item>) JsonConvert.DeserializeObject<List<Item>>(Resources.items_pt);
          break;
        case 7:
          this.ItemList = (List<Item>) JsonConvert.DeserializeObject<List<Item>>(Resources.items_jp);
          break;
        case 8:
          this.ItemList = (List<Item>) JsonConvert.DeserializeObject<List<Item>>(Resources.items_kr);
          break;
        case 9:
          this.ItemList = (List<Item>) JsonConvert.DeserializeObject<List<Item>>(Resources.items_cn);
          break;
        case 10:
          this.ItemList = (List<Item>) JsonConvert.DeserializeObject<List<Item>>(Resources.items_tw);
          break;
        case 11:
          this.ItemList = (List<Item>) JsonConvert.DeserializeObject<List<Item>>(Resources.items_th);
          break;
        case 12:
          this.ItemList = (List<Item>) JsonConvert.DeserializeObject<List<Item>>(Resources.items_tr);
          break;
        case 13:
          this.ItemList = (List<Item>) JsonConvert.DeserializeObject<List<Item>>(Resources.items_id);
          break;
        default:
          this.ItemList = (List<Item>) JsonConvert.DeserializeObject<List<Item>>(Resources.items_en);
          break;
      }
    }

    public Image GetImage(string url, int size)
    {
      using (CustomWebClient customWebClient = new CustomWebClient())
        return (Image) new Bitmap(Image.FromStream((Stream) new MemoryStream(customWebClient.DownloadData(url.Replace("bdocodex.com", "bddatabase.net")))), new Size(size, size));
    }

    public Color GetColor(int grade)
    {
      switch (grade)
      {
        case 0:
          return Color.FromArgb(220, 220, 220);
        case 1:
          return Color.FromArgb(0, (int) byte.MaxValue, 0);
        case 2:
          return Color.FromArgb(100, 100, (int) byte.MaxValue);
        case 3:
          return Color.FromArgb((int) byte.MaxValue, 180, 0);
        case 4:
          return Color.FromArgb((int) byte.MaxValue, 80, 0);
        default:
          return Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      }
    }

    private void listItems_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.listItems.SelectedItems.Count <= 0)
        return;
      ListViewItem listItem = this.listItems.SelectedItems[0];
      Item obj = this.ItemList.First<Item>((Func<Item, bool>) (x => x.Id == Convert.ToInt32(listItem.Name)));
      this.txtName.Text = listItem.Text;
      this.txtId.Text = listItem.Name;
      this.pnlGrade.BackColor = this.GetColor(obj.Grade);
      this.picItem.Image = this.GetImage(obj.Icon, 75);
      listItem.Selected = false;
    }

    private void txtSearch_TextChanged(object sender, EventArgs e)
    {
      this.timer.Stop();
      this.timer.Start();
    }

    private void Timer_Tick(object sender, EventArgs e)
    {
      this.timer.Stop();
      if (this.txtSearch.Text == "")
        return;
      this.listItems.Clear();
      this.listItems.LargeImageList.Images.Clear();
      try
      {
        int id = Convert.ToInt32(this.txtSearch.Text);
        this.ItemList.ForEach((Action<Item>) (x =>
        {
          if (x.Id == id)
          {
            this.listItems.LargeImageList.Images.Add(x.Id.ToString(), this.GetImage(x.Icon, 50));
            this.listItems.Items.Add(new ListViewItem()
            {
              Text = x.Name,
              Name = x.Id.ToString(),
              ImageKey = x.Id.ToString(),
              BackColor = this.GetColor(x.Grade)
            });
          }
          Application.DoEvents();
        }));
      }
      catch
      {
        this.ItemList.ForEach((Action<Item>) (x =>
        {
          if (x.Name.IndexOf(this.txtSearch.Text, StringComparison.OrdinalIgnoreCase) >= 0)
          {
            this.listItems.LargeImageList.Images.Add(x.Id.ToString(), this.GetImage(x.Icon, 50));
            this.listItems.Items.Add(new ListViewItem()
            {
              Text = x.Name,
              Name = x.Id.ToString(),
              ImageKey = x.Id.ToString(),
              BackColor = this.GetColor(x.Grade)
            });
          }
          Application.DoEvents();
        }));
      }
    }

    private void txt_Click(object sender, EventArgs e)
    {
      Clipboard.SetText((sender as TextBox).Text);
    }

    private void cmbLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.ChangeLanguage(this.cmbLanguage.SelectedIndex);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.txtSearch = new TextBox();
      this.listItems = new ListView();
      this.lblSearch = new Label();
      this.label1 = new Label();
      this.grpInfo = new GroupBox();
      this.pnlGrade = new Panel();
      this.picItem = new PictureBox();
      this.lblId = new Label();
      this.txtId = new TextBox();
      this.lblName = new Label();
      this.txtName = new TextBox();
      this.cmbLanguage = new ComboBox();
      this.lblLanguage = new Label();
      this.grpInfo.SuspendLayout();
      this.pnlGrade.SuspendLayout();
      ((ISupportInitialize) this.picItem).BeginInit();
      this.SuspendLayout();
      this.txtSearch.Location = new Point(15, 32);
      this.txtSearch.Name = "txtSearch";
      this.txtSearch.Size = new Size(305, 20);
      this.txtSearch.TabIndex = 0;
      this.txtSearch.TextChanged += new EventHandler(this.txtSearch_TextChanged);
      this.listItems.HideSelection = false;
      this.listItems.Location = new Point(15, 75);
      this.listItems.MultiSelect = false;
      this.listItems.Name = "listItems";
      this.listItems.Size = new Size(305, 320);
      this.listItems.TabIndex = 1;
      this.listItems.UseCompatibleStateImageBehavior = false;
      this.listItems.SelectedIndexChanged += new EventHandler(this.listItems_SelectedIndexChanged);
      this.lblSearch.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.lblSearch.Location = new Point(15, 12);
      this.lblSearch.Name = "lblSearch";
      this.lblSearch.Size = new Size(305, 17);
      this.lblSearch.TabIndex = 2;
      this.lblSearch.Text = "Enter ID or Name";
      this.lblSearch.TextAlign = ContentAlignment.MiddleCenter;
      this.label1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.label1.Location = new Point(15, 55);
      this.label1.Name = "label1";
      this.label1.Size = new Size(305, 17);
      this.label1.TabIndex = 3;
      this.label1.Text = "Item List";
      this.label1.TextAlign = ContentAlignment.MiddleCenter;
      this.grpInfo.BackColor = Color.White;
      this.grpInfo.Controls.Add((Control) this.pnlGrade);
      this.grpInfo.Controls.Add((Control) this.lblId);
      this.grpInfo.Controls.Add((Control) this.txtId);
      this.grpInfo.Controls.Add((Control) this.lblName);
      this.grpInfo.Controls.Add((Control) this.txtName);
      this.grpInfo.Location = new Point(15, 401);
      this.grpInfo.Name = "grpInfo";
      this.grpInfo.Size = new Size(305, 105);
      this.grpInfo.TabIndex = 4;
      this.grpInfo.TabStop = false;
      this.grpInfo.Text = "Item Information";
      this.pnlGrade.Controls.Add((Control) this.picItem);
      this.pnlGrade.Location = new Point(6, 19);
      this.pnlGrade.Name = "pnlGrade";
      this.pnlGrade.Size = new Size(80, 80);
      this.pnlGrade.TabIndex = 5;
      this.picItem.BackColor = Color.White;
      this.picItem.Location = new Point(3, 3);
      this.picItem.Name = "picItem";
      this.picItem.Size = new Size(74, 74);
      this.picItem.TabIndex = 0;
      this.picItem.TabStop = false;
      this.lblId.AutoSize = true;
      this.lblId.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.lblId.Location = new Point(92, 60);
      this.lblId.Name = "lblId";
      this.lblId.Size = new Size(48, 13);
      this.lblId.TabIndex = 4;
      this.lblId.Text = "Item ID";
      this.txtId.Location = new Point(92, 76);
      this.txtId.Name = "txtId";
      this.txtId.ReadOnly = true;
      this.txtId.Size = new Size(207, 20);
      this.txtId.TabIndex = 3;
      this.txtId.Click += new EventHandler(this.txt_Click);
      this.lblName.AutoSize = true;
      this.lblName.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.lblName.Location = new Point(92, 16);
      this.lblName.Name = "lblName";
      this.lblName.Size = new Size(67, 13);
      this.lblName.TabIndex = 2;
      this.lblName.Text = "Item Name";
      this.txtName.Location = new Point(92, 32);
      this.txtName.Name = "txtName";
      this.txtName.ReadOnly = true;
      this.txtName.Size = new Size(207, 20);
      this.txtName.TabIndex = 1;
      this.txtName.Click += new EventHandler(this.txt_Click);
      this.cmbLanguage.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbLanguage.FormattingEnabled = true;
      this.cmbLanguage.Items.AddRange(new object[14]
      {
        (object) "English",
        (object) "German",
        (object) "French",
        (object) "Russian",
        (object) "Spanish NA/EU",
        (object) "Spanish RedFox",
        (object) "Portuguese",
        (object) "Japanese",
        (object) "Korean",
        (object) "Chinese",
        (object) "Taiwanese",
        (object) "Thai",
        (object) "Turkish",
        (object) "Indonesian"
      });
      this.cmbLanguage.Location = new Point(104, 512);
      this.cmbLanguage.Name = "cmbLanguage";
      this.cmbLanguage.Size = new Size(216, 21);
      this.cmbLanguage.TabIndex = 5;
      this.cmbLanguage.SelectedIndexChanged += new EventHandler(this.cmbLanguage_SelectedIndexChanged);
      this.lblLanguage.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.lblLanguage.Location = new Point(12, 513);
      this.lblLanguage.Name = "lblLanguage";
      this.lblLanguage.Size = new Size(86, 17);
      this.lblLanguage.TabIndex = 6;
      this.lblLanguage.Text = "Language:";
      this.lblLanguage.TextAlign = ContentAlignment.MiddleCenter;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.White;
      this.ClientSize = new Size(336, 548);
      this.Controls.Add((Control) this.lblLanguage);
      this.Controls.Add((Control) this.cmbLanguage);
      this.Controls.Add((Control) this.grpInfo);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.lblSearch);
      this.Controls.Add((Control) this.listItems);
      this.Controls.Add((Control) this.txtSearch);
      this.Name = nameof (Form1);
      this.Padding = new Padding(12);
      this.Text = "BDDatabase - by Nopey";
      this.grpInfo.ResumeLayout(false);
      this.grpInfo.PerformLayout();
      this.pnlGrade.ResumeLayout(false);
      ((ISupportInitialize) this.picItem).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
