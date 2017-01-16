using System;
using System.Linq;
using System.Windows.Forms;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace StorageQueueDemo
{
    public partial class Form1 : Form
    {
        CloudQueue queue;

        public Form1()
        {
            InitializeComponent();
        }

        private Timer timer1;
        public void InitTimer()
        {
            timer1 = new Timer();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = 2000; // in miliseconds
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ReadFromQueue();
        }

        private async void InitializeQueue(string connectionString, string queueName)            //<<< (async)   initializes queue, create new or use existing
        {
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(connectionString);  
                                                                                            // ^ creates an object from your storage account
            CloudQueueClient queueClient = cloudStorageAccount.CreateCloudQueueClient();    // < creates a client queue
            queue = queueClient.GetQueueReference(queueName);                                  

            try { await queue.CreateIfNotExistsAsync(); }                                   // < creates the queue if it doesn't exist
            catch (Exception ex)
            { Console.WriteLine("Could not initialize queue! Error: " + ex.Message); return; }

            groupBox.Enabled = true;

            InitTimer();                                                                  // < fetches elements from the queue through the timer method (uses the ReadFromQueue(); function)
        }



        private async void ReadFromQueue()                                                       //<<< (async)   reads elements (peek) from the queue and puts it in the list
        {
            try { await queue.FetchAttributesAsync(); }                                     // < fetches items from queue(items)            
            catch (Exception ex)
            { Console.WriteLine("Could not read from queue! Error: " + ex.Message); return; }

            int? items = queue.ApproximateMessageCount;                                    

            if (items > 0)
            {
                var messages = await queue.PeekMessagesAsync(items.Value);                // < Reads the elements(Peek), without the other suers losing access

                messagelist.Items.Clear();

                for (int i = 0; i < messages.ToArray().Length; i++)
                    messagelist.Items.Add(messages.ToArray()[i].AsString);              // < Puts the elements on the list

                pictureBox1.Image = StorageQueueDemo.Properties.Resources._in;

            }
            else if (items == 0)
            {
                pictureBox1.Image = StorageQueueDemo.Properties.Resources._out;
                messagelist.Items.Clear();
            }

        }

        private async void AddMessage(string text)                                      //<<< (async)   Puts a new element on the queue and refreshes
        {
            CloudQueueMessage melding = new CloudQueueMessage(text);                        // < Creates an object to the queue with a text
            try { await queue.AddMessageAsync(melding); }                                   // < Sends the object to the queue
            catch (Exception ex)
            { Console.WriteLine("Could not add message to queue! Error: " + ex.Message); return; }

            ReadFromQueue();                                                                     // < Reloads the elements
        }
        
        private async void DeleteMessages()                                                 //<<< (async)   Removes all elements from the queue
        {
            try { await queue.ClearAsync(); }
            catch (Exception ex)
            { Console.WriteLine("Could not delete messages from queue! Error: " + ex.Message); return; }

            ReadFromQueue();
        }

        private void buttonAdd_Click(object sender, EventArgs e)                         //<<< (Button)  Sends a message on the queue
        {
            AddMessage(messagetext.Text);
            messagetext.Text = "";
        }

        private void buttonConnect_Click(object sender, EventArgs e)                        //<<< (Button)  Connects and initializes
        {
            InitializeQueue(tekstConnectionString.Text, tekstQueueName.Text);   
        }

        private void buttonRefresh_Click(object sender, EventArgs e)                         //<<< (Button)  Refreshes
        {
            ReadFromQueue();
        }

        private void buttonDelete_Click(object sender, EventArgs e)                           //<<< (Button)  Removes all elements
        {
            DeleteMessages();
        }

    }
}
