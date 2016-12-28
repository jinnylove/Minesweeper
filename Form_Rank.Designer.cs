namespace Minesweeper
{
    partial class Form_Rank
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Button_Reset = new System.Windows.Forms.Button();
            this.Button_OK = new System.Windows.Forms.Button();
            this.Label_Beginner = new System.Windows.Forms.Label();
            this.Label_Intermediate = new System.Windows.Forms.Label();
            this.Label_Expert = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(86, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Beginner:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(86, 134);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(166, 24);
            this.label2.TabIndex = 1;
            this.label2.Text = "Intermediate:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(86, 198);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 24);
            this.label3.TabIndex = 2;
            this.label3.Text = "Expert:";
            // 
            // Button_Reset
            // 
            this.Button_Reset.Location = new System.Drawing.Point(90, 291);
            this.Button_Reset.Name = "Button_Reset";
            this.Button_Reset.Size = new System.Drawing.Size(112, 43);
            this.Button_Reset.TabIndex = 3;
            this.Button_Reset.Text = "Reset";
            this.Button_Reset.UseVisualStyleBackColor = true;
            this.Button_Reset.Click += new System.EventHandler(this.Button_Reset_Click);
            // 
            // Button_OK
            // 
            this.Button_OK.Location = new System.Drawing.Point(289, 291);
            this.Button_OK.Name = "Button_OK";
            this.Button_OK.Size = new System.Drawing.Size(96, 43);
            this.Button_OK.TabIndex = 4;
            this.Button_OK.Text = "OK";
            this.Button_OK.UseVisualStyleBackColor = true;
            this.Button_OK.Click += new System.EventHandler(this.Button_OK_Click);
            // 
            // Label_Beginner
            // 
            this.Label_Beginner.AutoSize = true;
            this.Label_Beginner.Location = new System.Drawing.Point(329, 71);
            this.Label_Beginner.Name = "Label_Beginner";
            this.Label_Beginner.Size = new System.Drawing.Size(22, 24);
            this.Label_Beginner.TabIndex = 5;
            this.Label_Beginner.Text = "0";
            // 
            // Label_Intermediate
            // 
            this.Label_Intermediate.AutoSize = true;
            this.Label_Intermediate.Location = new System.Drawing.Point(326, 134);
            this.Label_Intermediate.Name = "Label_Intermediate";
            this.Label_Intermediate.Size = new System.Drawing.Size(22, 24);
            this.Label_Intermediate.TabIndex = 6;
            this.Label_Intermediate.Text = "0";
            // 
            // Label_Expert
            // 
            this.Label_Expert.AutoSize = true;
            this.Label_Expert.Location = new System.Drawing.Point(326, 197);
            this.Label_Expert.Name = "Label_Expert";
            this.Label_Expert.Size = new System.Drawing.Size(22, 24);
            this.Label_Expert.TabIndex = 7;
            this.Label_Expert.Text = "0";
            // 
            // Form_Rank
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 391);
            this.Controls.Add(this.Label_Expert);
            this.Controls.Add(this.Label_Intermediate);
            this.Controls.Add(this.Label_Beginner);
            this.Controls.Add(this.Button_OK);
            this.Controls.Add(this.Button_Reset);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form_Rank";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Minesweeper";
            this.Load += new System.EventHandler(this.Form_Rank_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Button_Reset;
        private System.Windows.Forms.Button Button_OK;
        private System.Windows.Forms.Label Label_Beginner;
        private System.Windows.Forms.Label Label_Intermediate;
        private System.Windows.Forms.Label Label_Expert;
    }
}