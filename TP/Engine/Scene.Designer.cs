namespace Engine
{
    partial class Scene
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.steppingTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // steppingTimer
            // 
            this.steppingTimer.Interval = 16;
            this.steppingTimer.Tick += new System.EventHandler(this.steppingTimer_Tick);
            // 
            // Scene
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "Scene";
            this.Load += new System.EventHandler(this.Scene_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Scene_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Scene_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Scene_KeyUp);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Scene_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Scene_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Scene_MouseUp);
            this.Resize += new System.EventHandler(this.Scene_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer steppingTimer;
    }
}
