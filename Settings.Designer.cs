using System;

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.RemoveSave_box = new System.Windows.Forms.CheckBox();
            this.RemoveRebuildGraphics_box = new System.Windows.Forms.CheckBox();
            this.SettingsOverride = new System.Windows.Forms.GroupBox();
            this.UnbindLabel = new System.Windows.Forms.Label();
            this.UnbindChatButton_box = new System.Windows.Forms.CheckBox();
            this.UnbindEmoteButton_box = new System.Windows.Forms.CheckBox();
            this.SlingshotModeLabel = new System.Windows.Forms.Label();
            this.SlingshotMode_dropdown = new System.Windows.Forms.ComboBox();
            this.AdvancedCrafting_box = new System.Windows.Forms.CheckBox();
            this.ToolHitLocations_box = new System.Windows.Forms.CheckBox();
            this.EnableZoomButtons_box = new System.Windows.Forms.CheckBox();
            this.FootstepVolumeLabel = new System.Windows.Forms.Label();
            this.AmbientVolumeLabel = new System.Windows.Forms.Label();
            this.SoundVolumeLabel = new System.Windows.Forms.Label();
            this.FootstepVolume = new System.Windows.Forms.TrackBar();
            this.AmbientVolume = new System.Windows.Forms.TrackBar();
            this.SoundVolume = new System.Windows.Forms.TrackBar();
            this.MusicVolumeLabel = new System.Windows.Forms.Label();
            this.MusicVolume = new System.Windows.Forms.TrackBar();
            this.EnableSettingsOverride_box = new System.Windows.Forms.CheckBox();
            this.splitAssignView = new System.Windows.Forms.DataGridView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.SplitName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Trigger = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.TriggerValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SettingsOverride.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FootstepVolume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AmbientVolume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SoundVolume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MusicVolume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitAssignView)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // RemoveSave_box
            // 
            this.RemoveSave_box.AutoSize = true;
            this.RemoveSave_box.Location = new System.Drawing.Point(4, 18);
            this.RemoveSave_box.Margin = new System.Windows.Forms.Padding(6);
            this.RemoveSave_box.Name = "RemoveSave_box";
            this.RemoveSave_box.Size = new System.Drawing.Size(238, 29);
            this.RemoveSave_box.TabIndex = 2;
            this.RemoveSave_box.Text = "Remove saving time";
            this.RemoveSave_box.UseVisualStyleBackColor = true;
            // 
            // RemoveRebuildGraphics_box
            // 
            this.RemoveRebuildGraphics_box.AutoSize = true;
            this.RemoveRebuildGraphics_box.Location = new System.Drawing.Point(4, 59);
            this.RemoveRebuildGraphics_box.Margin = new System.Windows.Forms.Padding(6);
            this.RemoveRebuildGraphics_box.Name = "RemoveRebuildGraphics_box";
            this.RemoveRebuildGraphics_box.Size = new System.Drawing.Size(328, 29);
            this.RemoveRebuildGraphics_box.TabIndex = 3;
            this.RemoveRebuildGraphics_box.Text = "Remove rebuild graphics time";
            this.RemoveRebuildGraphics_box.UseVisualStyleBackColor = true;
            // 
            // SettingsOverride
            // 
            this.SettingsOverride.Controls.Add(this.UnbindLabel);
            this.SettingsOverride.Controls.Add(this.UnbindChatButton_box);
            this.SettingsOverride.Controls.Add(this.UnbindEmoteButton_box);
            this.SettingsOverride.Controls.Add(this.SlingshotModeLabel);
            this.SettingsOverride.Controls.Add(this.SlingshotMode_dropdown);
            this.SettingsOverride.Controls.Add(this.AdvancedCrafting_box);
            this.SettingsOverride.Controls.Add(this.ToolHitLocations_box);
            this.SettingsOverride.Controls.Add(this.EnableZoomButtons_box);
            this.SettingsOverride.Controls.Add(this.FootstepVolumeLabel);
            this.SettingsOverride.Controls.Add(this.AmbientVolumeLabel);
            this.SettingsOverride.Controls.Add(this.SoundVolumeLabel);
            this.SettingsOverride.Controls.Add(this.FootstepVolume);
            this.SettingsOverride.Controls.Add(this.AmbientVolume);
            this.SettingsOverride.Controls.Add(this.SoundVolume);
            this.SettingsOverride.Controls.Add(this.MusicVolumeLabel);
            this.SettingsOverride.Controls.Add(this.MusicVolume);
            this.SettingsOverride.Location = new System.Drawing.Point(7, 7);
            this.SettingsOverride.Margin = new System.Windows.Forms.Padding(4);
            this.SettingsOverride.Name = "SettingsOverride";
            this.SettingsOverride.Padding = new System.Windows.Forms.Padding(4);
            this.SettingsOverride.Size = new System.Drawing.Size(642, 703);
            this.SettingsOverride.TabIndex = 6;
            this.SettingsOverride.TabStop = false;
            this.SettingsOverride.Text = "Settings Override";
            // 
            // UnbindLabel
            // 
            this.UnbindLabel.AutoSize = true;
            this.UnbindLabel.Location = new System.Drawing.Point(71, 571);
            this.UnbindLabel.Name = "UnbindLabel";
            this.UnbindLabel.Size = new System.Drawing.Size(459, 25);
            this.UnbindLabel.TabIndex = 15;
            this.UnbindLabel.Text = "Unbind Buttons (does not affect v1.5.5 or later)";
            // 
            // UnbindChatButton_box
            // 
            this.UnbindChatButton_box.AutoSize = true;
            this.UnbindChatButton_box.Location = new System.Drawing.Point(43, 644);
            this.UnbindChatButton_box.Name = "UnbindChatButton_box";
            this.UnbindChatButton_box.Size = new System.Drawing.Size(453, 29);
            this.UnbindChatButton_box.TabIndex = 14;
            this.UnbindChatButton_box.Text = "Unbind Chat Button (only T, / will still work)";
            this.UnbindChatButton_box.UseVisualStyleBackColor = true;
            // 
            // UnbindEmoteButton_box
            // 
            this.UnbindEmoteButton_box.AutoSize = true;
            this.UnbindEmoteButton_box.Location = new System.Drawing.Point(43, 609);
            this.UnbindEmoteButton_box.Name = "UnbindEmoteButton_box";
            this.UnbindEmoteButton_box.Size = new System.Drawing.Size(247, 29);
            this.UnbindEmoteButton_box.TabIndex = 13;
            this.UnbindEmoteButton_box.Text = "Unbind Emote Button";
            this.UnbindEmoteButton_box.UseVisualStyleBackColor = true;
            // 
            // SlingshotModeLabel
            // 
            this.SlingshotModeLabel.AutoSize = true;
            this.SlingshotModeLabel.Location = new System.Drawing.Point(71, 481);
            this.SlingshotModeLabel.Name = "SlingshotModeLabel";
            this.SlingshotModeLabel.Size = new System.Drawing.Size(338, 25);
            this.SlingshotModeLabel.TabIndex = 12;
            this.SlingshotModeLabel.Text = "Slingshot Fire Mode (v1.4 or later)";
            // 
            // SlingshotMode_dropdown
            // 
            this.SlingshotMode_dropdown.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.SlingshotMode_dropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SlingshotMode_dropdown.FormattingEnabled = true;
            this.SlingshotMode_dropdown.Items.AddRange(new object[] {
            "Hold and release",
            "Pull in opposite direction"});
            this.SlingshotMode_dropdown.Location = new System.Drawing.Point(43, 513);
            this.SlingshotMode_dropdown.Name = "SlingshotMode_dropdown";
            this.SlingshotMode_dropdown.Size = new System.Drawing.Size(444, 33);
            this.SlingshotMode_dropdown.TabIndex = 11;
            // 
            // AdvancedCrafting_box
            // 
            this.AdvancedCrafting_box.AutoSize = true;
            this.AdvancedCrafting_box.Location = new System.Drawing.Point(43, 444);
            this.AdvancedCrafting_box.Name = "AdvancedCrafting_box";
            this.AdvancedCrafting_box.Size = new System.Drawing.Size(262, 29);
            this.AdvancedCrafting_box.TabIndex = 10;
            this.AdvancedCrafting_box.Text = "Advanced Crafting Info";
            this.AdvancedCrafting_box.UseVisualStyleBackColor = true;
            // 
            // ToolHitLocations_box
            // 
            this.ToolHitLocations_box.AutoSize = true;
            this.ToolHitLocations_box.Location = new System.Drawing.Point(43, 409);
            this.ToolHitLocations_box.Name = "ToolHitLocations_box";
            this.ToolHitLocations_box.Size = new System.Drawing.Size(350, 29);
            this.ToolHitLocations_box.TabIndex = 9;
            this.ToolHitLocations_box.Text = "Always Show Tool Hit Locations";
            this.ToolHitLocations_box.UseVisualStyleBackColor = true;
            // 
            // EnableZoomButtons_box
            // 
            this.EnableZoomButtons_box.AutoSize = true;
            this.EnableZoomButtons_box.Location = new System.Drawing.Point(43, 374);
            this.EnableZoomButtons_box.Name = "EnableZoomButtons_box";
            this.EnableZoomButtons_box.Size = new System.Drawing.Size(250, 29);
            this.EnableZoomButtons_box.TabIndex = 8;
            this.EnableZoomButtons_box.Text = "Enable Zoom Buttons";
            this.EnableZoomButtons_box.UseVisualStyleBackColor = true;
            // 
            // FootstepVolumeLabel
            // 
            this.FootstepVolumeLabel.AutoSize = true;
            this.FootstepVolumeLabel.Location = new System.Drawing.Point(38, 304);
            this.FootstepVolumeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.FootstepVolumeLabel.Name = "FootstepVolumeLabel";
            this.FootstepVolumeLabel.Size = new System.Drawing.Size(96, 25);
            this.FootstepVolumeLabel.TabIndex = 7;
            this.FootstepVolumeLabel.Text = "Footstep";
            // 
            // AmbientVolumeLabel
            // 
            this.AmbientVolumeLabel.AutoSize = true;
            this.AmbientVolumeLabel.Location = new System.Drawing.Point(38, 221);
            this.AmbientVolumeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.AmbientVolumeLabel.Name = "AmbientVolumeLabel";
            this.AmbientVolumeLabel.Size = new System.Drawing.Size(90, 25);
            this.AmbientVolumeLabel.TabIndex = 6;
            this.AmbientVolumeLabel.Text = "Ambient";
            // 
            // SoundVolumeLabel
            // 
            this.SoundVolumeLabel.AutoSize = true;
            this.SoundVolumeLabel.Location = new System.Drawing.Point(38, 138);
            this.SoundVolumeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.SoundVolumeLabel.Name = "SoundVolumeLabel";
            this.SoundVolumeLabel.Size = new System.Drawing.Size(74, 25);
            this.SoundVolumeLabel.TabIndex = 5;
            this.SoundVolumeLabel.Text = "Sound";
            // 
            // FootstepVolume
            // 
            this.FootstepVolume.Location = new System.Drawing.Point(152, 306);
            this.FootstepVolume.Margin = new System.Windows.Forms.Padding(4);
            this.FootstepVolume.Maximum = 100;
            this.FootstepVolume.Name = "FootstepVolume";
            this.FootstepVolume.Size = new System.Drawing.Size(460, 90);
            this.FootstepVolume.TabIndex = 4;
            this.FootstepVolume.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // AmbientVolume
            // 
            this.AmbientVolume.Location = new System.Drawing.Point(152, 219);
            this.AmbientVolume.Margin = new System.Windows.Forms.Padding(4);
            this.AmbientVolume.Maximum = 100;
            this.AmbientVolume.Name = "AmbientVolume";
            this.AmbientVolume.Size = new System.Drawing.Size(460, 90);
            this.AmbientVolume.TabIndex = 3;
            this.AmbientVolume.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // SoundVolume
            // 
            this.SoundVolume.Location = new System.Drawing.Point(152, 133);
            this.SoundVolume.Margin = new System.Windows.Forms.Padding(4);
            this.SoundVolume.Maximum = 100;
            this.SoundVolume.Name = "SoundVolume";
            this.SoundVolume.Size = new System.Drawing.Size(460, 90);
            this.SoundVolume.TabIndex = 2;
            this.SoundVolume.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // MusicVolumeLabel
            // 
            this.MusicVolumeLabel.AutoSize = true;
            this.MusicVolumeLabel.Location = new System.Drawing.Point(38, 56);
            this.MusicVolumeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.MusicVolumeLabel.Name = "MusicVolumeLabel";
            this.MusicVolumeLabel.Size = new System.Drawing.Size(69, 25);
            this.MusicVolumeLabel.TabIndex = 1;
            this.MusicVolumeLabel.Text = "Music";
            // 
            // MusicVolume
            // 
            this.MusicVolume.Location = new System.Drawing.Point(152, 46);
            this.MusicVolume.Margin = new System.Windows.Forms.Padding(4);
            this.MusicVolume.Maximum = 100;
            this.MusicVolume.Name = "MusicVolume";
            this.MusicVolume.Size = new System.Drawing.Size(460, 90);
            this.MusicVolume.TabIndex = 0;
            this.MusicVolume.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // EnableSettingsOverride_box
            // 
            this.EnableSettingsOverride_box.AutoSize = true;
            this.EnableSettingsOverride_box.Location = new System.Drawing.Point(4, 98);
            this.EnableSettingsOverride_box.Margin = new System.Windows.Forms.Padding(4);
            this.EnableSettingsOverride_box.Name = "EnableSettingsOverride_box";
            this.EnableSettingsOverride_box.Size = new System.Drawing.Size(283, 29);
            this.EnableSettingsOverride_box.TabIndex = 7;
            this.EnableSettingsOverride_box.Text = "Enable Settings Override";
            this.EnableSettingsOverride_box.UseVisualStyleBackColor = true;
            // 
            // splitAssignView
            // 
            this.splitAssignView.AllowUserToAddRows = false;
            this.splitAssignView.AllowUserToDeleteRows = false;
            this.splitAssignView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.splitAssignView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.splitAssignView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.splitAssignView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SplitName,
            this.Trigger,
            this.TriggerValue});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.Format = "N0";
            dataGridViewCellStyle2.NullValue = "0";
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.splitAssignView.DefaultCellStyle = dataGridViewCellStyle2;
            this.splitAssignView.Location = new System.Drawing.Point(3, 6);
            this.splitAssignView.MultiSelect = false;
            this.splitAssignView.Name = "splitAssignView";
            this.splitAssignView.RowHeadersVisible = false;
            this.splitAssignView.RowHeadersWidth = 82;
            this.splitAssignView.RowTemplate.Height = 33;
            this.splitAssignView.ShowEditingIcon = false;
            this.splitAssignView.Size = new System.Drawing.Size(867, 769);
            this.splitAssignView.TabIndex = 8;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(19, 134);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(892, 828);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tabControl1.TabIndex = 9;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.SettingsOverride);
            this.tabPage1.Location = new System.Drawing.Point(8, 39);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(876, 781);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Settings";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.splitAssignView);
            this.tabPage2.Location = new System.Drawing.Point(8, 39);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(876, 781);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Splits";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // SplitName
            // 
            this.SplitName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.SplitName.HeaderText = "Split Name";
            this.SplitName.MinimumWidth = 40;
            this.SplitName.Name = "SplitName";
            this.SplitName.ReadOnly = true;
            // 
            // Trigger
            // 
            this.Trigger.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Trigger.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Trigger.HeaderText = "Trigger";
            this.Trigger.MinimumWidth = 150;
            this.Trigger.Name = "Trigger";
            this.Trigger.Width = 150;
            // 
            // TriggerValue
            // 
            this.TriggerValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.TriggerValue.HeaderText = "Day/Floor";
            this.TriggerValue.MinimumWidth = 10;
            this.TriggerValue.Name = "TriggerValue";
            this.TriggerValue.Width = 150;
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.EnableSettingsOverride_box);
            this.Controls.Add(this.RemoveRebuildGraphics_box);
            this.Controls.Add(this.RemoveSave_box);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "Settings";
            this.Size = new System.Drawing.Size(940, 1003);
            this.SettingsOverride.ResumeLayout(false);
            this.SettingsOverride.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FootstepVolume)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AmbientVolume)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SoundVolume)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MusicVolume)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitAssignView)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox RemoveSave_box;
        private System.Windows.Forms.CheckBox RemoveRebuildGraphics_box;
        private System.Windows.Forms.GroupBox SettingsOverride;
        private System.Windows.Forms.TrackBar MusicVolume;
        private System.Windows.Forms.CheckBox EnableSettingsOverride_box;
        private System.Windows.Forms.Label FootstepVolumeLabel;
        private System.Windows.Forms.Label AmbientVolumeLabel;
        private System.Windows.Forms.Label SoundVolumeLabel;
        private System.Windows.Forms.TrackBar FootstepVolume;
        private System.Windows.Forms.TrackBar AmbientVolume;
        private System.Windows.Forms.TrackBar SoundVolume;
        private System.Windows.Forms.Label MusicVolumeLabel;
        private System.Windows.Forms.ComboBox SlingshotMode_dropdown;
        private System.Windows.Forms.CheckBox AdvancedCrafting_box;
        private System.Windows.Forms.CheckBox ToolHitLocations_box;
        private System.Windows.Forms.CheckBox EnableZoomButtons_box;
        private System.Windows.Forms.Label SlingshotModeLabel;
        private System.Windows.Forms.CheckBox UnbindChatButton_box;
        private System.Windows.Forms.CheckBox UnbindEmoteButton_box;
        private System.Windows.Forms.Label UnbindLabel;
        private System.Windows.Forms.DataGridView splitAssignView;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridViewTextBoxColumn SplitName;
        private System.Windows.Forms.DataGridViewComboBoxColumn Trigger;
        private System.Windows.Forms.DataGridViewTextBoxColumn TriggerValue;
    }
}
