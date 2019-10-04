namespace HospitalApp
{
    partial class FrmMain
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.hastalarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.randevularımToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ayarlarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hastanelerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bölümlerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.doktorlarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hastalarToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hastalarToolStripMenuItem,
            this.ayarlarToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(633, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // hastalarToolStripMenuItem
            // 
            this.hastalarToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.randevularımToolStripMenuItem});
            this.hastalarToolStripMenuItem.Name = "hastalarToolStripMenuItem";
            this.hastalarToolStripMenuItem.Size = new System.Drawing.Size(78, 20);
            this.hastalarToolStripMenuItem.Text = "Randevular";
            // 
            // randevularımToolStripMenuItem
            // 
            this.randevularımToolStripMenuItem.Name = "randevularımToolStripMenuItem";
            this.randevularımToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.randevularımToolStripMenuItem.Text = "Randevularım...";
            this.randevularımToolStripMenuItem.Click += new System.EventHandler(this.RandevularımToolStripMenuItem_Click);
            // 
            // ayarlarToolStripMenuItem
            // 
            this.ayarlarToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hastanelerToolStripMenuItem,
            this.bölümlerToolStripMenuItem,
            this.doktorlarToolStripMenuItem,
            this.hastalarToolStripMenuItem1});
            this.ayarlarToolStripMenuItem.Name = "ayarlarToolStripMenuItem";
            this.ayarlarToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.ayarlarToolStripMenuItem.Text = "Ayarlar";
            // 
            // hastanelerToolStripMenuItem
            // 
            this.hastanelerToolStripMenuItem.Name = "hastanelerToolStripMenuItem";
            this.hastanelerToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.hastanelerToolStripMenuItem.Text = "Hastaneler...";
            this.hastanelerToolStripMenuItem.Click += new System.EventHandler(this.HastanelerToolStripMenuItem_Click);
            // 
            // bölümlerToolStripMenuItem
            // 
            this.bölümlerToolStripMenuItem.Name = "bölümlerToolStripMenuItem";
            this.bölümlerToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.bölümlerToolStripMenuItem.Text = "Bölümler...";
            this.bölümlerToolStripMenuItem.Click += new System.EventHandler(this.BölümlerToolStripMenuItem_Click);
            // 
            // doktorlarToolStripMenuItem
            // 
            this.doktorlarToolStripMenuItem.Name = "doktorlarToolStripMenuItem";
            this.doktorlarToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.doktorlarToolStripMenuItem.Text = "Doktorlar...";
            this.doktorlarToolStripMenuItem.Click += new System.EventHandler(this.DoktorlarToolStripMenuItem_Click);
            // 
            // hastalarToolStripMenuItem1
            // 
            this.hastalarToolStripMenuItem1.Name = "hastalarToolStripMenuItem1";
            this.hastalarToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.hastalarToolStripMenuItem1.Text = "Hastalar...";
            this.hastalarToolStripMenuItem1.Click += new System.EventHandler(this.HastalarToolStripMenuItem1_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(633, 321);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmMain";
            this.Text = "Hastane Randevu";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem hastalarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem randevularımToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ayarlarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hastanelerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bölümlerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem doktorlarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hastalarToolStripMenuItem1;
    }
}

