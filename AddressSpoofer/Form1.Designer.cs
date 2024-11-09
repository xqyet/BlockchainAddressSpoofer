using System.Drawing;
using System.Windows.Forms;

namespace AddressSpoofer
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.targetPrefixLabel = new System.Windows.Forms.Label();
            this.targetPrefixTextBox = new System.Windows.Forms.TextBox();
            this.startButton = new System.Windows.Forms.Button();
            this.abortButton = new System.Windows.Forms.Button();
            this.attemptsLabel = new System.Windows.Forms.Label();
            this.resultsTextBox = new System.Windows.Forms.RichTextBox();
            this.transactionAnalyticsLabel = new System.Windows.Forms.Label();
            this.analyticsAddressTextBox = new System.Windows.Forms.TextBox();
            this.fetchTransactionsButton = new System.Windows.Forms.Button();
            this.analyticsResultsDataGridView = new System.Windows.Forms.DataGridView();

            // Icons and Logos
            this.addressSpooferLogo = new System.Windows.Forms.PictureBox();
            this.bscScanPictureBox = new System.Windows.Forms.PictureBox();
            this.etherscanPictureBox = new System.Windows.Forms.PictureBox();

            // Info Label above BSCScan logo
            this.infoLabel = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.analyticsResultsDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.addressSpooferLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bscScanPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.etherscanPictureBox)).BeginInit();

            this.SuspendLayout();

            // Set the form icon
            this.Icon = new Icon(@"C:\Users\giova\source\repos\AddressSpoofer\AddressSpoofer\images\favicon.ico");

            // Target Address Label
            this.targetPrefixLabel.AutoSize = true;
            this.targetPrefixLabel.Location = new System.Drawing.Point(20, 20);
            this.targetPrefixLabel.Name = "targetPrefixLabel";
            this.targetPrefixLabel.Size = new System.Drawing.Size(110, 20);
            this.targetPrefixLabel.Text = "Target Address:";

            // Target Address TextBox
            this.targetPrefixTextBox.Location = new System.Drawing.Point(20, 45);
            this.targetPrefixTextBox.Size = new System.Drawing.Size(600, 27);

            // Start Button
            this.startButton.Location = new System.Drawing.Point(20, 85);
            this.startButton.Size = new System.Drawing.Size(100, 35);
            this.startButton.Text = "Start";
            this.startButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);

            // Abort Button
            this.abortButton.Location = new System.Drawing.Point(130, 85);
            this.abortButton.Size = new System.Drawing.Size(100, 35);
            this.abortButton.Text = "Abort";
            this.abortButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.abortButton.Click += new System.EventHandler(this.abortButton_Click);

            // Attempts Label
            this.attemptsLabel.Location = new System.Drawing.Point(250, 90);
            this.attemptsLabel.Size = new System.Drawing.Size(100, 20);

            // Results TextBox
            this.resultsTextBox.Location = new System.Drawing.Point(20, 130);
            this.resultsTextBox.Size = new System.Drawing.Size(600, 400);

            // Transaction Analytics Label
            this.transactionAnalyticsLabel.AutoSize = true;
            this.transactionAnalyticsLabel.Location = new System.Drawing.Point(650, 20);
            this.transactionAnalyticsLabel.Name = "transactionAnalyticsLabel";
            this.transactionAnalyticsLabel.Size = new System.Drawing.Size(160, 20);
            this.transactionAnalyticsLabel.Text = "Transaction Analytics";

            // Analytics Address TextBox
            this.analyticsAddressTextBox.Location = new System.Drawing.Point(650, 45);
            this.analyticsAddressTextBox.Size = new System.Drawing.Size(500, 27);
            this.analyticsAddressTextBox.Text = "Enter Ethereum address here";

            // Fetch Transactions Button
            this.fetchTransactionsButton.Location = new System.Drawing.Point(1160, 45);
            this.fetchTransactionsButton.Size = new System.Drawing.Size(150, 35);
            this.fetchTransactionsButton.Text = "Fetch Transactions";
            this.fetchTransactionsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.fetchTransactionsButton.Click += new System.EventHandler(this.fetchTransactionsButton_Click);

            // Analytics Results DataGridView
            this.analyticsResultsDataGridView.Location = new System.Drawing.Point(650, 90);
            this.analyticsResultsDataGridView.Size = new System.Drawing.Size(900, 600);
            this.analyticsResultsDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.analyticsResultsDataGridView.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.analyticsResultsDataGridView.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.analyticsResultsDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.analyticsResultsDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.analyticsResultsDataGridView.RowHeadersVisible = false;
            this.analyticsResultsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.analyticsResultsDataGridView.ColumnCount = 6;

            // Set column headers for DataGridView
            this.analyticsResultsDataGridView.Columns[0].Name = "Transaction Hash";
            this.analyticsResultsDataGridView.Columns[1].Name = "From Wallet";
            this.analyticsResultsDataGridView.Columns[2].Name = "To Wallet";
            this.analyticsResultsDataGridView.Columns[3].Name = "Direction";
            this.analyticsResultsDataGridView.Columns[4].Name = "Value (ETH)";
            this.analyticsResultsDataGridView.Columns[5].Name = "Date/Time";

            // Set CellContentClick event handler for clickable addresses
            this.analyticsResultsDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.analyticsResultsDataGridView_CellContentClick);
            this.analyticsResultsDataGridView.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.analyticsResultsDataGridView_CellMouseEnter);
            this.analyticsResultsDataGridView.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.analyticsResultsDataGridView_CellMouseLeave);

            // Info Label above BSCScan logo
            this.infoLabel.AutoSize = true;
            this.infoLabel.Location = new System.Drawing.Point(30, 590); // Adjusted position for wrap effect
            this.infoLabel.Size = new System.Drawing.Size(500, 60); // Wrap text
            this.infoLabel.Text = "\"This is a proof of concept program developed by xqyet.\nIf you have any questions about this program and/or adding\nan open source automation framework, please reach out to him at www.xqyet.dev\"";
            this.infoLabel.TextAlign = ContentAlignment.MiddleCenter;
            this.infoLabel.Font = new Font(this.infoLabel.Font, FontStyle.Italic);

            // Address Spoofer Logo
            this.addressSpooferLogo.Location = new System.Drawing.Point(1100, 700);  // Bottom right hand side
            this.addressSpooferLogo.Size = new System.Drawing.Size(600, 240);  // Adjustable size
            this.addressSpooferLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.addressSpooferLogo.Image = Image.FromFile(@"C:\Users\giova\source\repos\AddressSpoofer\AddressSpoofer\images\address_spoofer_logo.jpg");

            // BSCScan PictureBox
            this.bscScanPictureBox.Location = new System.Drawing.Point(20, 750);
            this.bscScanPictureBox.Size = new System.Drawing.Size(150, 50);
            this.bscScanPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.bscScanPictureBox.Image = Image.FromFile(@"C:\Users\giova\source\repos\AddressSpoofer\AddressSpoofer\images\bscscan.png");
            this.bscScanPictureBox.Click += (s, e) => System.Diagnostics.Process.Start("https://bscscan.com/txs");

            // Etherscan PictureBox
            this.etherscanPictureBox.Location = new System.Drawing.Point(20, 810);
            this.etherscanPictureBox.Size = new System.Drawing.Size(165, 55);
            this.etherscanPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.etherscanPictureBox.Image = Image.FromFile(@"C:\Users\giova\source\repos\AddressSpoofer\AddressSpoofer\images\etherscan-logo.png");
            this.etherscanPictureBox.Click += (s, e) => System.Diagnostics.Process.Start("https://etherscan.io/txs");

            // Form1 Settings
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1600, 1000);
            this.Controls.Add(this.targetPrefixLabel);
            this.Controls.Add(this.targetPrefixTextBox);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.abortButton);
            this.Controls.Add(this.attemptsLabel);
            this.Controls.Add(this.resultsTextBox);
            this.Controls.Add(this.transactionAnalyticsLabel);
            this.Controls.Add(this.analyticsAddressTextBox);
            this.Controls.Add(this.fetchTransactionsButton);
            this.Controls.Add(this.analyticsResultsDataGridView);
            this.Controls.Add(this.infoLabel);
            this.Controls.Add(this.addressSpooferLogo);
            this.Controls.Add(this.bscScanPictureBox);
            this.Controls.Add(this.etherscanPictureBox);
            this.Name = "Form1";
            this.Text = "Address Spoofer - xqyet";

            ((System.ComponentModel.ISupportInitialize)(this.analyticsResultsDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.addressSpooferLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bscScanPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.etherscanPictureBox)).EndInit();

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label targetPrefixLabel;
        private System.Windows.Forms.TextBox targetPrefixTextBox;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button abortButton;
        private System.Windows.Forms.Label attemptsLabel;
        private System.Windows.Forms.RichTextBox resultsTextBox;
        private System.Windows.Forms.Label transactionAnalyticsLabel;
        private System.Windows.Forms.TextBox analyticsAddressTextBox;
        private System.Windows.Forms.Button fetchTransactionsButton;
        private System.Windows.Forms.DataGridView analyticsResultsDataGridView;
        private System.Windows.Forms.PictureBox addressSpooferLogo;
        private System.Windows.Forms.PictureBox bscScanPictureBox;
        private System.Windows.Forms.PictureBox etherscanPictureBox;
        private System.Windows.Forms.Label infoLabel;
    }
}
