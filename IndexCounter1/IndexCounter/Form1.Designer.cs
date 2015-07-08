namespace IndexCounter
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.minAgeBox = new System.Windows.Forms.TextBox();
            this.maxAgeBox = new System.Windows.Forms.TextBox();
            this.getLarButton = new System.Windows.Forms.Button();
            this.minAge = new System.Windows.Forms.Label();
            this.maxAge = new System.Windows.Forms.Label();
            this.manRB = new System.Windows.Forms.RadioButton();
            this.womanRB = new System.Windows.Forms.RadioButton();
            this.externalRB = new System.Windows.Forms.RadioButton();
            this.internalRB = new System.Windows.Forms.RadioButton();
            this.sexGroupBox = new System.Windows.Forms.GroupBox();
            this.radGroupBox = new System.Windows.Forms.GroupBox();
            this.sexGroupBox.SuspendLayout();
            this.radGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // minAgeBox
            // 
            this.minAgeBox.Location = new System.Drawing.Point(12, 46);
            this.minAgeBox.Name = "minAgeBox";
            this.minAgeBox.Size = new System.Drawing.Size(128, 20);
            this.minAgeBox.TabIndex = 0;
            this.minAgeBox.TextChanged += new System.EventHandler(this.minAgeBox_TextChanged);
            // 
            // maxAgeBox
            // 
            this.maxAgeBox.Location = new System.Drawing.Point(152, 46);
            this.maxAgeBox.Name = "maxAgeBox";
            this.maxAgeBox.Size = new System.Drawing.Size(128, 20);
            this.maxAgeBox.TabIndex = 1;
            this.maxAgeBox.TextChanged += new System.EventHandler(this.maxAgeBox_TextChanged);
            // 
            // getLarButton
            // 
            this.getLarButton.Location = new System.Drawing.Point(94, 147);
            this.getLarButton.Name = "getLarButton";
            this.getLarButton.Size = new System.Drawing.Size(107, 30);
            this.getLarButton.TabIndex = 2;
            this.getLarButton.Text = "Получить LAR";
            this.getLarButton.UseVisualStyleBackColor = true;
            this.getLarButton.Click += new System.EventHandler(this.getLarButton_Click);
            // 
            // minAge
            // 
            this.minAge.AutoSize = true;
            this.minAge.Location = new System.Drawing.Point(9, 30);
            this.minAge.Name = "minAge";
            this.minAge.Size = new System.Drawing.Size(124, 13);
            this.minAge.TabIndex = 3;
            this.minAge.Text = "Минимальный возраст";
            // 
            // maxAge
            // 
            this.maxAge.AutoSize = true;
            this.maxAge.Location = new System.Drawing.Point(150, 30);
            this.maxAge.Name = "maxAge";
            this.maxAge.Size = new System.Drawing.Size(130, 13);
            this.maxAge.TabIndex = 4;
            this.maxAge.Text = "Максимальный возраст";
            // 
            // manRB
            // 
            this.manRB.AutoSize = true;
            this.manRB.Location = new System.Drawing.Point(6, 19);
            this.manRB.Name = "manRB";
            this.manRB.Size = new System.Drawing.Size(72, 17);
            this.manRB.TabIndex = 6;
            this.manRB.TabStop = true;
            this.manRB.Text = "Мужчины";
            this.manRB.UseVisualStyleBackColor = true;
            this.manRB.CheckedChanged += new System.EventHandler(this.manRB_CheckedChanged);
            // 
            // womanRB
            // 
            this.womanRB.AutoSize = true;
            this.womanRB.Location = new System.Drawing.Point(6, 38);
            this.womanRB.Name = "womanRB";
            this.womanRB.Size = new System.Drawing.Size(77, 17);
            this.womanRB.TabIndex = 7;
            this.womanRB.TabStop = true;
            this.womanRB.Text = "Женщины";
            this.womanRB.UseVisualStyleBackColor = true;
            this.womanRB.CheckedChanged += new System.EventHandler(this.womanRB_CheckedChanged);
            // 
            // externalRB
            // 
            this.externalRB.AutoSize = true;
            this.externalRB.Location = new System.Drawing.Point(6, 19);
            this.externalRB.Name = "externalRB";
            this.externalRB.Size = new System.Drawing.Size(70, 17);
            this.externalRB.TabIndex = 8;
            this.externalRB.TabStop = true;
            this.externalRB.Text = "Внешнее";
            this.externalRB.UseVisualStyleBackColor = true;
            this.externalRB.CheckedChanged += new System.EventHandler(this.externalRB_CheckedChanged);
            // 
            // internalRB
            // 
            this.internalRB.AutoSize = true;
            this.internalRB.Location = new System.Drawing.Point(6, 38);
            this.internalRB.Name = "internalRB";
            this.internalRB.Size = new System.Drawing.Size(84, 17);
            this.internalRB.TabIndex = 9;
            this.internalRB.TabStop = true;
            this.internalRB.Text = "Внутреннее";
            this.internalRB.UseVisualStyleBackColor = true;
            // 
            // sexGroupBox
            // 
            this.sexGroupBox.Controls.Add(this.manRB);
            this.sexGroupBox.Controls.Add(this.womanRB);
            this.sexGroupBox.Location = new System.Drawing.Point(12, 72);
            this.sexGroupBox.Name = "sexGroupBox";
            this.sexGroupBox.Size = new System.Drawing.Size(128, 69);
            this.sexGroupBox.TabIndex = 10;
            this.sexGroupBox.TabStop = false;
            this.sexGroupBox.Text = "Пол";
            this.sexGroupBox.Enter += new System.EventHandler(this.sexGroupBox_Enter);
            // 
            // radGroupBox
            // 
            this.radGroupBox.Controls.Add(this.externalRB);
            this.radGroupBox.Controls.Add(this.internalRB);
            this.radGroupBox.Location = new System.Drawing.Point(152, 72);
            this.radGroupBox.Name = "radGroupBox";
            this.radGroupBox.Size = new System.Drawing.Size(128, 69);
            this.radGroupBox.TabIndex = 11;
            this.radGroupBox.TabStop = false;
            this.radGroupBox.Text = "Облучение";
            this.radGroupBox.Enter += new System.EventHandler(this.radGroupBox_Enter);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(290, 189);
            this.Controls.Add(this.radGroupBox);
            this.Controls.Add(this.sexGroupBox);
            this.Controls.Add(this.maxAge);
            this.Controls.Add(this.minAge);
            this.Controls.Add(this.getLarButton);
            this.Controls.Add(this.maxAgeBox);
            this.Controls.Add(this.minAgeBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.sexGroupBox.ResumeLayout(false);
            this.sexGroupBox.PerformLayout();
            this.radGroupBox.ResumeLayout(false);
            this.radGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox minAgeBox;
        private System.Windows.Forms.TextBox maxAgeBox;
        private System.Windows.Forms.Button getLarButton;
        private System.Windows.Forms.Label minAge;
        private System.Windows.Forms.Label maxAge;
        private System.Windows.Forms.RadioButton manRB;
        private System.Windows.Forms.RadioButton womanRB;
        private System.Windows.Forms.RadioButton externalRB;
        private System.Windows.Forms.RadioButton internalRB;
        private System.Windows.Forms.GroupBox sexGroupBox;
        private System.Windows.Forms.GroupBox radGroupBox;
    }
}

