namespace typeFall
{
    partial class HomeForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox = new System.Windows.Forms.TextBox();
            this.tutorialText = new System.Windows.Forms.Label();
            this.scoreText = new System.Windows.Forms.Label();
            this.highscore = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox
            // 
            this.textBox.BackColor = System.Drawing.SystemColors.HotTrack;
            this.textBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBox.Cursor = System.Windows.Forms.Cursors.No;
            this.textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox.Location = new System.Drawing.Point(249, 810);
            this.textBox.MaxLength = 2;
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(351, 62);
            this.textBox.TabIndex = 0;
            this.textBox.TabStop = false;
            this.textBox.Text = "...";
            this.textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // tutorialText
            // 
            this.tutorialText.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tutorialText.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tutorialText.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.tutorialText.Location = new System.Drawing.Point(67, 102);
            this.tutorialText.Name = "tutorialText";
            this.tutorialText.Size = new System.Drawing.Size(709, 675);
            this.tutorialText.TabIndex = 1;
            this.tutorialText.Text = "tutorialText";
            // 
            // scoreText
            // 
            this.scoreText.AutoSize = true;
            this.scoreText.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.scoreText.Font = new System.Drawing.Font("Microsoft Sans Serif", 32.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scoreText.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.scoreText.Location = new System.Drawing.Point(12, 825);
            this.scoreText.Name = "scoreText";
            this.scoreText.Size = new System.Drawing.Size(46, 51);
            this.scoreText.TabIndex = 2;
            this.scoreText.Text = "0";
            this.scoreText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.scoreText.Visible = false;
            // 
            // highscore
            // 
            this.highscore.AutoSize = true;
            this.highscore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.highscore.Font = new System.Drawing.Font("Microsoft Sans Serif", 32.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.highscore.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.highscore.Location = new System.Drawing.Point(128, 825);
            this.highscore.Name = "highscore";
            this.highscore.Size = new System.Drawing.Size(0, 51);
            this.highscore.TabIndex = 3;
            this.highscore.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.highscore.Visible = false;
            // 
            // HomeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(856, 899);
            this.Controls.Add(this.highscore);
            this.Controls.Add(this.scoreText);
            this.Controls.Add(this.tutorialText);
            this.Controls.Add(this.textBox);
            this.Name = "HomeForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.HomeFormLoad);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.HomeForm_KeyPress);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Label tutorialText;
        private System.Windows.Forms.Label scoreText;
        private System.Windows.Forms.Label highscore;
    }
}

