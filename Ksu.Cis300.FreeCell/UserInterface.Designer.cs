namespace Ksu.Cis300.FreeCell
{
    partial class UserInterface
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
            this.uxMenuBar = new System.Windows.Forms.MenuStrip();
            this.uxNewGame = new System.Windows.Forms.ToolStripMenuItem();
            this.uxGameNumber = new System.Windows.Forms.ToolStripTextBox();
            this.uxMoveAllHome = new System.Windows.Forms.ToolStripMenuItem();
            this.uxSeed = new System.Windows.Forms.NumericUpDown();
            this.uxMainPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.uxTopPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.uxFreeCellPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.uxHomePanel = new System.Windows.Forms.FlowLayoutPanel();
            this.uxTableauPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.uxMenuBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uxSeed)).BeginInit();
            this.uxMainPanel.SuspendLayout();
            this.uxTopPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // uxMenuBar
            // 
            this.uxMenuBar.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.uxMenuBar.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.uxMenuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uxNewGame,
            this.uxGameNumber,
            this.uxMoveAllHome});
            this.uxMenuBar.Location = new System.Drawing.Point(0, 0);
            this.uxMenuBar.Name = "uxMenuBar";
            this.uxMenuBar.Padding = new System.Windows.Forms.Padding(4, 1, 0, 1);
            this.uxMenuBar.Size = new System.Drawing.Size(800, 36);
            this.uxMenuBar.TabIndex = 0;
            this.uxMenuBar.Text = "menuStrip1";
            // 
            // uxNewGame
            // 
            this.uxNewGame.Name = "uxNewGame";
            this.uxNewGame.Size = new System.Drawing.Size(114, 34);
            this.uxNewGame.Text = "New Game";
            this.uxNewGame.Click += new System.EventHandler(this.uxNewGame_Click);
            // 
            // uxGameNumber
            // 
            this.uxGameNumber.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.uxGameNumber.Name = "uxGameNumber";
            this.uxGameNumber.ReadOnly = true;
            this.uxGameNumber.Size = new System.Drawing.Size(115, 34);
            this.uxGameNumber.Text = "Game Number:";
            this.uxGameNumber.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // uxMoveAllHome
            // 
            this.uxMoveAllHome.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.uxMoveAllHome.Name = "uxMoveAllHome";
            this.uxMoveAllHome.Size = new System.Drawing.Size(152, 34);
            this.uxMoveAllHome.Text = "Move All Home";
            this.uxMoveAllHome.Click += new System.EventHandler(this.uxMoveAllHome_Click);
            // 
            // uxSeed
            // 
            this.uxSeed.Location = new System.Drawing.Point(205, 3);
            this.uxSeed.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.uxSeed.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.uxSeed.Name = "uxSeed";
            this.uxSeed.Size = new System.Drawing.Size(86, 20);
            this.uxSeed.TabIndex = 1;
            this.uxSeed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.uxSeed.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.uxSeed.ValueChanged += new System.EventHandler(this.uxSeed_ValueChanged);
            // 
            // uxMainPanel
            // 
            this.uxMainPanel.AutoSize = true;
            this.uxMainPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.uxMainPanel.Controls.Add(this.uxTopPanel);
            this.uxMainPanel.Controls.Add(this.uxTableauPanel);
            this.uxMainPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.uxMainPanel.Location = new System.Drawing.Point(5, 25);
            this.uxMainPanel.Name = "uxMainPanel";
            this.uxMainPanel.Size = new System.Drawing.Size(118, 118);
            this.uxMainPanel.TabIndex = 2;
            // 
            // uxTopPanel
            // 
            this.uxTopPanel.AutoSize = true;
            this.uxTopPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.uxTopPanel.Controls.Add(this.uxFreeCellPanel);
            this.uxTopPanel.Controls.Add(this.uxHomePanel);
            this.uxTopPanel.Location = new System.Drawing.Point(3, 3);
            this.uxTopPanel.Name = "uxTopPanel";
            this.uxTopPanel.Size = new System.Drawing.Size(112, 56);
            this.uxTopPanel.TabIndex = 3;
            // 
            // uxFreeCellPanel
            // 
            this.uxFreeCellPanel.AutoSize = true;
            this.uxFreeCellPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.uxFreeCellPanel.Location = new System.Drawing.Point(3, 3);
            this.uxFreeCellPanel.MinimumSize = new System.Drawing.Size(50, 50);
            this.uxFreeCellPanel.Name = "uxFreeCellPanel";
            this.uxFreeCellPanel.Size = new System.Drawing.Size(50, 50);
            this.uxFreeCellPanel.TabIndex = 3;
            // 
            // uxHomePanel
            // 
            this.uxHomePanel.AutoSize = true;
            this.uxHomePanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.uxHomePanel.Location = new System.Drawing.Point(59, 3);
            this.uxHomePanel.MinimumSize = new System.Drawing.Size(50, 50);
            this.uxHomePanel.Name = "uxHomePanel";
            this.uxHomePanel.Size = new System.Drawing.Size(50, 50);
            this.uxHomePanel.TabIndex = 3;
            // 
            // uxTableauPanel
            // 
            this.uxTableauPanel.AutoSize = true;
            this.uxTableauPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.uxTableauPanel.Location = new System.Drawing.Point(3, 65);
            this.uxTableauPanel.MinimumSize = new System.Drawing.Size(100, 50);
            this.uxTableauPanel.Name = "uxTableauPanel";
            this.uxTableauPanel.Size = new System.Drawing.Size(100, 50);
            this.uxTableauPanel.TabIndex = 3;
            // 
            // UserInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.DarkGreen;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.uxMainPanel);
            this.Controls.Add(this.uxSeed);
            this.Controls.Add(this.uxMenuBar);
            this.MaximizeBox = false;
            this.Name = "UserInterface";
            this.Text = "FreeCell";
            this.uxMenuBar.ResumeLayout(false);
            this.uxMenuBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uxSeed)).EndInit();
            this.uxMainPanel.ResumeLayout(false);
            this.uxMainPanel.PerformLayout();
            this.uxTopPanel.ResumeLayout(false);
            this.uxTopPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip uxMenuBar;
        private System.Windows.Forms.ToolStripMenuItem uxNewGame;
        private System.Windows.Forms.ToolStripTextBox uxGameNumber;
        private System.Windows.Forms.ToolStripMenuItem uxMoveAllHome;
        private System.Windows.Forms.NumericUpDown uxSeed;
        private System.Windows.Forms.FlowLayoutPanel uxMainPanel;
        private System.Windows.Forms.FlowLayoutPanel uxTopPanel;
        private System.Windows.Forms.FlowLayoutPanel uxFreeCellPanel;
        private System.Windows.Forms.FlowLayoutPanel uxHomePanel;
        private System.Windows.Forms.FlowLayoutPanel uxTableauPanel;
    }
}

