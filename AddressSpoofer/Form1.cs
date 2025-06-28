using System;
using System.Drawing;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Nethereum.Signer;
using Nethereum.Util;
using Newtonsoft.Json.Linq;

namespace AddressSpoofer
{
    public partial class Form1 : Form
    {
        private static volatile bool isMatchFound = false;
        private static int attempts = 0;
        private int bestMatchLength = 0;
        private string targetPrefix;
        private System.Windows.Forms.Timer timer;
        private CancellationTokenSource cts;

        private const string etherscanApiKey = "api_key"; //etherscan api key required, will need in order to pull transaction history

        public Form1()
        {
            InitializeComponent();

            // Set the initial window size
            this.Size = new Size(1100, 620);
            this.StartPosition = FormStartPosition.CenterScreen;

            this.BackColor = Color.White;
            this.ForeColor = Color.Black;

            // Set up textboxes with placeholder handling
            targetPrefixTextBox.BackColor = Color.White;
            targetPrefixTextBox.ForeColor = Color.Gray;
            targetPrefixTextBox.Text = "Enter full target address here";
            targetPrefixTextBox.Enter += TargetPrefixTextBox_Enter;
            targetPrefixTextBox.Leave += TargetPrefixTextBox_Leave;

            analyticsAddressTextBox.BackColor = Color.White;
            analyticsAddressTextBox.ForeColor = Color.Gray;
            analyticsAddressTextBox.Text = "Enter Ethereum address here";
            analyticsAddressTextBox.Enter += AnalyticsAddressTextBox_Enter;
            analyticsAddressTextBox.Leave += AnalyticsAddressTextBox_Leave;

            // Adjust DataGridView appearance
            analyticsResultsDataGridView.BackgroundColor = Color.White;
            analyticsResultsDataGridView.DefaultCellStyle.BackColor = Color.White;
            analyticsResultsDataGridView.DefaultCellStyle.ForeColor = Color.Black;
            analyticsResultsDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Initialize timer for attempts counter
            timer = new System.Windows.Forms.Timer { Interval = 5000 };
            timer.Tick += Timer_Tick;
        }

        private void homeButton_Click(object sender, EventArgs e)
        {
            SetButtonHighlight(homeButton);
            // Show or reset the form view as needed for "Home"
        }

        private void SetButtonHighlight(Button button)
        {
            // Reset all buttons to default color
            homeButton.BackColor = Color.White;
            placeholderButton1.BackColor = Color.White;
            placeholderButton2.BackColor = Color.White;
            placeholderButton3.BackColor = Color.White;

            // Highlight the active button
            button.BackColor = Color.LightBlue; // Adjust the highlight color as needed
        }

        // Apply highlight effects for each button on mouse hover
        private void Button_MouseEnter(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button != null) button.BackColor = Color.LightGray;
        }

        private void Button_MouseLeave(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button != null && button.BackColor != Color.LightBlue) // Keep highlight if selected
                button.BackColor = Color.White;
        }

        private async void startButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(targetPrefixTextBox.Text) || targetPrefixTextBox.Text == "Enter full target address here")
            {
                MessageBox.Show("Please enter a valid target address.");
                return;
            }

            targetPrefix = targetPrefixTextBox.Text.Substring(2); // Remove "0x" if present
            isMatchFound = false;
            attempts = 0;
            bestMatchLength = 0;
            attemptsLabel.Text = "Attempts: 0";

            cts = new CancellationTokenSource();

            timer.Start();

            try
            {
                await Task.Run(() => RunVanityAddressGenerator(cts.Token));
            }
            catch (OperationCanceledException)
            {
                MessageBox.Show("Address search was canceled.");
            }
            finally
            {
                timer.Stop();
            }
        }

        private void RunVanityAddressGenerator(CancellationToken token)
        {
            int taskCount = Environment.ProcessorCount * 2;
            Parallel.For(0, taskCount, (i, state) =>
            {
                if (token.IsCancellationRequested)
                {
                    state.Stop();
                    return;
                }
                GenerateMatchingBnbAddress(targetPrefix, token);
            });
        }

        private void abortButton_Click(object sender, EventArgs e)
        {
            cts?.Cancel();
            MessageBox.Show("Search aborted.");
        }

        private void GenerateMatchingBnbAddress(string targetPrefix, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                var ecKey = EthECKey.GenerateKey();
                var address = ecKey.GetPublicAddress().Substring(2);

                int matchLength = 0;
                for (int i = 0; i < Math.Min(targetPrefix.Length, address.Length); i++)
                {
                    if (address[i] == targetPrefix[i])
                        matchLength++;
                    else
                        break;
                }

                if (matchLength > bestMatchLength)
                {
                    bestMatchLength = matchLength;
                    string privateKeyHex = ecKey.GetPrivateKey();

                    this.Invoke((Action)(() =>
                    {
                        resultsTextBox.AppendText($"Match with {bestMatchLength} characters!\nTotal Attempts: {attempts}\nPrivate Key: {privateKeyHex}\nBNB Address: 0x{address}\n\n");
                    }));
                }

                Interlocked.Increment(ref attempts);
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            attemptsLabel.Text = $"Attempts: {attempts}";
        }

        private async void fetchTransactionsButton_Click(object sender, EventArgs e)
        {
            await LoadTransactionHistory(analyticsAddressTextBox.Text.Trim());
        }

        private async Task LoadTransactionHistory(string address)
        {
            analyticsResultsDataGridView.Rows.Clear();

            if (string.IsNullOrWhiteSpace(address))
            {
                MessageBox.Show("Please enter a valid Ethereum address.");
                return;
            }

            try
            {
                string apiUrl = $"https://api.etherscan.io/api?module=account&action=txlist&address={address}&startblock=0&endblock=99999999&sort=asc&apikey={etherscanApiKey}";

                using (HttpClient client = new HttpClient())
                {
                    string json = await client.GetStringAsync(apiUrl);
                    JObject jsonResponse = JObject.Parse(json);

                    if (jsonResponse["status"]?.ToString() == "1" && jsonResponse["result"] is JArray transactions)
                    {
                        foreach (var tx in transactions)
                        {
                            string hash = tx["hash"]?.ToString();
                            string from = tx["from"]?.ToString();
                            string to = tx["to"]?.ToString();
                            string valueInEther = (decimal.Parse(tx["value"]?.ToString() ?? "0") / 1_000_000_000_000_000_000).ToString("0.0000");
                            string direction = (from.ToLower() == address.ToLower()) ? "OUT" : "IN";
                            Color directionColor = (direction == "OUT") ? Color.Red : Color.Green;

                            DateTime dateTime = DateTimeOffset.FromUnixTimeSeconds(long.Parse(tx["timeStamp"]?.ToString() ?? "0")).DateTime;
                            string dateFormatted = dateTime.ToString("yyyy-MM-dd HH:mm:ss");

                            int rowIndex = analyticsResultsDataGridView.Rows.Add(hash, from, to, direction, valueInEther, dateFormatted);

                            // Set color for From and To cells
                            analyticsResultsDataGridView.Rows[rowIndex].Cells[1].Style.ForeColor = Color.Blue;
                            analyticsResultsDataGridView.Rows[rowIndex].Cells[2].Style.ForeColor = Color.Blue;

                            // Set color for Direction cell
                            analyticsResultsDataGridView.Rows[rowIndex].Cells[3].Style.ForeColor = directionColor;
                        }
                    }
                    else
                    {
                        analyticsResultsDataGridView.Rows.Add("No transactions found or an error occurred.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private async void analyticsResultsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Check if clicked cell is in the "From Wallet" or "To Wallet" columns
                if (e.ColumnIndex == 1 || e.ColumnIndex == 2)
                {
                    string address = analyticsResultsDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    await LoadTransactionHistory(address); // Load history for clicked address
                }
            }
        }

        private void analyticsResultsDataGridView_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && (e.ColumnIndex == 1 || e.ColumnIndex == 2))
            {
                analyticsResultsDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.LightBlue;
            }
        }

        private void analyticsResultsDataGridView_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && (e.ColumnIndex == 1 || e.ColumnIndex == 2))
            {
                analyticsResultsDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.White;
            }
        }

        private void TargetPrefixTextBox_Enter(object sender, EventArgs e)
        {
            if (targetPrefixTextBox.Text == "Enter full target address here")
            {
                targetPrefixTextBox.Text = "";
                targetPrefixTextBox.ForeColor = Color.Black;
            }
        }

        private void TargetPrefixTextBox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(targetPrefixTextBox.Text))
            {
                targetPrefixTextBox.Text = "Enter full target address here";
                targetPrefixTextBox.ForeColor = Color.Gray;
            }
        }

        private void AnalyticsAddressTextBox_Enter(object sender, EventArgs e)
        {
            if (analyticsAddressTextBox.Text == "Enter Ethereum address here")
            {
                analyticsAddressTextBox.Text = "";
                analyticsAddressTextBox.ForeColor = Color.Black;
            }
        }

        private void AnalyticsAddressTextBox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(analyticsAddressTextBox.Text))
            {
                analyticsAddressTextBox.Text = "Enter Ethereum address here";
                analyticsAddressTextBox.ForeColor = Color.Gray;
            }
        }
    }
}
