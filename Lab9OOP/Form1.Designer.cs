namespace Lab9OOP
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            userNameTextBox = new TextBox();
            titleTextBox = new TextBox();
            priceTextBox = new TextBox();
            loginButton = new Button();
            logoutButton = new Button();
            publishButton = new Button();
            writeButton = new Button();
            adsListBox = new ListBox();

            SuspendLayout();

            // label1 — "Имя:"
            label1.AutoSize = true;
            label1.Location = new Point(12, 15);
            label1.Size = new Size(37, 15);
            label1.Text = "Имя:";

            // userNameTextBox
            userNameTextBox.Location = new Point(55, 12);
            userNameTextBox.Name = "userNameTextBox";
            userNameTextBox.Size = new Size(150, 23);

            // loginButton
            loginButton.Location = new Point(211, 10);
            loginButton.Size = new Size(75, 25);
            loginButton.Text = "Войти";
            loginButton.UseVisualStyleBackColor = true;
            loginButton.Click += loginButton_Click;

            // logoutButton
            logoutButton.Enabled = false;
            logoutButton.Location = new Point(292, 10);
            logoutButton.Size = new Size(75, 25);
            logoutButton.Text = "Выйти";
            logoutButton.UseVisualStyleBackColor = true;
            logoutButton.Click += logoutButton_Click;

            // label2 — "Товар:"
            label2.AutoSize = true;
            label2.Location = new Point(12, 50);
            label2.Size = new Size(46, 15);
            label2.Text = "Товар:";

            // titleTextBox
            titleTextBox.Location = new Point(64, 47);
            titleTextBox.Name = "titleTextBox";
            titleTextBox.Size = new Size(250, 23);

            // label3 — "Цена:"
            label3.AutoSize = true;
            label3.Location = new Point(320, 50);
            label3.Size = new Size(38, 15);
            label3.Text = "Цена:";

            // priceTextBox
            priceTextBox.Location = new Point(364, 47);
            priceTextBox.Name = "priceTextBox";
            priceTextBox.Size = new Size(100, 23);

            // publishButton
            publishButton.Enabled = false;
            publishButton.Location = new Point(470, 45);
            publishButton.Size = new Size(130, 26);
            publishButton.Text = "Опубликовать";
            publishButton.UseVisualStyleBackColor = true;
            publishButton.Click += publishButton_Click;

            // label4 — "Доска объявлений:"
            label4.AutoSize = true;
            label4.Location = new Point(12, 85);
            label4.Size = new Size(110, 15);
            label4.Text = "Доска объявлений:";

            // adsListBox
            adsListBox.FormattingEnabled = true;
            adsListBox.ItemHeight = 15;
            adsListBox.Location = new Point(12, 103);
            adsListBox.Name = "adsListBox";
            adsListBox.Size = new Size(460, 289);
            adsListBox.SelectedIndexChanged += adsListBox_SelectedIndexChanged;

            // writeButton — "Написать"
            writeButton.Enabled = false;
            writeButton.Location = new Point(478, 103);
            writeButton.Size = new Size(122, 30);
            writeButton.Text = "Написать";
            writeButton.UseVisualStyleBackColor = true;
            writeButton.Click += writeButton_Click;

            // Form1
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(612, 404);
            Controls.Add(writeButton);
            Controls.Add(label4);
            Controls.Add(adsListBox);
            Controls.Add(publishButton);
            Controls.Add(priceTextBox);
            Controls.Add(label3);
            Controls.Add(titleTextBox);
            Controls.Add(label2);
            Controls.Add(logoutButton);
            Controls.Add(loginButton);
            Controls.Add(userNameTextBox);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Доска объявлений";
            FormClosing += Form1_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }

        private Label label1 = null!;
        private Label label2 = null!;
        private Label label3 = null!;
        private Label label4 = null!;
        private TextBox userNameTextBox = null!;
        private TextBox titleTextBox = null!;
        private TextBox priceTextBox = null!;
        private Button loginButton = null!;
        private Button logoutButton = null!;
        private Button publishButton = null!;
        private Button writeButton = null!;
        private ListBox adsListBox = null!;
    }
}