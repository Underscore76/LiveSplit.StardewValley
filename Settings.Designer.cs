namespace LiveSplit.StardewValley
{
    partial class Settings
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
            this.RemovePause_box = new System.Windows.Forms.CheckBox();
            this.RemoveSave_box = new System.Windows.Forms.CheckBox();
            this.RemoveRebuildGraphics_box = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // RemovePause_box
            // 
            this.RemovePause_box.AutoSize = true;
            this.RemovePause_box.Location = new System.Drawing.Point(3, 3);
            this.RemovePause_box.Name = "RemovePause_box";
            this.RemovePause_box.Size = new System.Drawing.Size(125, 17);
            this.RemovePause_box.TabIndex = 1;
            this.RemovePause_box.Text = "Remove /pause time";
            this.RemovePause_box.UseVisualStyleBackColor = true;
            // 
            // RemoveSave_box
            // 
            this.RemoveSave_box.AutoSize = true;
            this.RemoveSave_box.Location = new System.Drawing.Point(3, 26);
            this.RemoveSave_box.Name = "RemoveSave_box";
            this.RemoveSave_box.Size = new System.Drawing.Size(122, 17);
            this.RemoveSave_box.TabIndex = 2;
            this.RemoveSave_box.Text = "Remove saving time";
            this.RemoveSave_box.UseVisualStyleBackColor = true;
            // 
            // RemoveRebuildGraphics_box
            // 
            this.RemoveRebuildGraphics_box.AutoSize = true;
            this.RemoveRebuildGraphics_box.Location = new System.Drawing.Point(3, 49);
            this.RemoveRebuildGraphics_box.Name = "RemoveRebuildGraphics_box";
            this.RemoveRebuildGraphics_box.Size = new System.Drawing.Size(165, 17);
            this.RemoveRebuildGraphics_box.TabIndex = 3;
            this.RemoveRebuildGraphics_box.Text = "Remove rebuild graphics time";
            this.RemoveRebuildGraphics_box.UseVisualStyleBackColor = true;
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.RemoveRebuildGraphics_box);
            this.Controls.Add(this.RemovePause_box);
            this.Controls.Add(this.RemoveSave_box);
            this.Name = "Settings";
            this.Size = new System.Drawing.Size(300, 300);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox RemovePause_box;
        private System.Windows.Forms.CheckBox RemoveSave_box;
        private System.Windows.Forms.CheckBox RemoveRebuildGraphics_box;
    }
}
