namespace Engine.Profiling
{
    partial class TallyResults
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
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.scene = new Engine.Scene();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // scene
            // 
            this.scene.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.scene.BackColor = System.Drawing.Color.White;
            this.scene.Location = new System.Drawing.Point(0, 0);
            this.scene.Margin = new System.Windows.Forms.Padding(0);
            this.scene.Name = "scene";
            this.scene.Size = new System.Drawing.Size(682, 303);
            this.scene.TabIndex = 0;
            this.scene.Resize += new System.EventHandler(this.scene_Resize);
            // 
            // TallyResults
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(682, 304);
            this.Controls.Add(this.scene);
            this.Name = "TallyResults";
            this.Text = "TallyResults";
            this.Load += new System.EventHandler(this.TallyResults_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Engine.Scene scene;
        private System.Windows.Forms.Timer timer1;
    }
}