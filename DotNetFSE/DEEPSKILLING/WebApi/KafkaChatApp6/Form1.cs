using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Confluent.Kafka;

namespace KafkaChatApp6
{
    public partial class Form1 : Form
    {
        ProducerConfig producerConfig = new ProducerConfig
        {
            BootstrapServers = "localhost:9092"
        };

        ConsumerConfig consumerConfig = new ConsumerConfig
        {
            BootstrapServers = "localhost:9092",
            GroupId = "chat-group",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        public Form1()
        {
            InitializeComponent();
            StartConsumer();
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMessage.Text))
                return;

            using (var producer = new ProducerBuilder<Null, string>(producerConfig).Build())
            {
                await producer.ProduceAsync("chatapp",
                    new Message<Null, string>
                    {
                        Value = txtMessage.Text
                    });

                txtMessage.Clear();
            }
        }

        private void StartConsumer()
        {
            Task.Run(() =>
            {
                using (var consumer = new ConsumerBuilder<Ignore, string>(consumerConfig).Build())
                {
                    consumer.Subscribe("chatapp");

                    while (true)
                    {
                        var result = consumer.Consume();

                        this.Invoke(new Action(() =>
                        {
                            lstMessages.Items.Add(result.Message.Value);
                        }));
                    }
                }
            });
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}