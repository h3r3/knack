/*
 *  Copyright 2004, 2005, 2006, 2007, 2008 Riccardo Gerosa.
 *
 *  This file is part of Knack.
 *
 *  Knack is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  Knack is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with Knack.  If not, see <http://www.gnu.org/licenses/>.
 * 
 */

using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace H3.Knack.UI
{
	/// <summary>
	/// Knack About Form
	/// </summary>
	public class AboutForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label labelAuthor;
		private System.Windows.Forms.Label labelVersion;
		private System.Windows.Forms.Label labelLicense;
		private System.Windows.Forms.Label labelKnack;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.LinkLabel linkLabel;
		private System.Windows.Forms.Button buttonOk;
		private System.Windows.Forms.Label labelStage;
		public AboutForm()
		{
			// The InitializeComponent() call is required for Windows Forms designer support.
			InitializeComponent();
			// Add constructor code after the InitializeComponent() call.
		}
		
		#region Windows Forms Designer generated code
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent() {
            this.labelStage = new System.Windows.Forms.Label();
            this.buttonOk = new System.Windows.Forms.Button();
            this.linkLabel = new System.Windows.Forms.LinkLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelKnack = new System.Windows.Forms.Label();
            this.labelLicense = new System.Windows.Forms.Label();
            this.labelVersion = new System.Windows.Forms.Label();
            this.labelAuthor = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelStage
            // 
            this.labelStage.AutoSize = true;
            this.labelStage.Font = new System.Drawing.Font("Microsoft Sans Serif", 2.9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Millimeter, ((byte)(0)));
            this.labelStage.Location = new System.Drawing.Point(205, 23);
            this.labelStage.Name = "labelStage";
            this.labelStage.Size = new System.Drawing.Size(108, 20);
            this.labelStage.TabIndex = 2;
            this.labelStage.Text = "alpha version";
            // 
            // buttonOk
            // 
            this.buttonOk.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonOk.Location = new System.Drawing.Point(256, 12);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(120, 33);
            this.buttonOk.TabIndex = 1;
            this.buttonOk.Text = "OK";
            this.buttonOk.Click += new System.EventHandler(this.ButtonOkClick);
            // 
            // linkLabel
            // 
            this.linkLabel.AutoSize = true;
            this.linkLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 2.9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Millimeter, ((byte)(0)));
            this.linkLabel.Location = new System.Drawing.Point(38, 163);
            this.linkLabel.Name = "linkLabel";
            this.linkLabel.Size = new System.Drawing.Size(189, 20);
            this.linkLabel.TabIndex = 6;
            this.linkLabel.TabStop = true;
            this.linkLabel.Text = "Go to Knack HomePage";
            this.linkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelLinkClicked);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 213);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(428, 59);
            this.panel1.TabIndex = 5;
            // 
            // labelKnack
            // 
            this.labelKnack.AutoSize = true;
            this.labelKnack.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Millimeter);
            this.labelKnack.Location = new System.Drawing.Point(26, 23);
            this.labelKnack.Name = "labelKnack";
            this.labelKnack.Size = new System.Drawing.Size(165, 56);
            this.labelKnack.TabIndex = 1;
            this.labelKnack.Text = "Knack";
            // 
            // labelLicense
            // 
            this.labelLicense.AutoSize = true;
            this.labelLicense.Font = new System.Drawing.Font("Microsoft Sans Serif", 2.9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Millimeter, ((byte)(0)));
            this.labelLicense.Location = new System.Drawing.Point(205, 78);
            this.labelLicense.Name = "labelLicense";
            this.labelLicense.Size = new System.Drawing.Size(186, 20);
            this.labelLicense.TabIndex = 8;
            this.labelLicense.Text = "Free Software - GPL v3";
            // 
            // labelVersion
            // 
            this.labelVersion.AutoSize = true;
            this.labelVersion.Font = new System.Drawing.Font("Tahoma", 2.9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Millimeter);
            this.labelVersion.Location = new System.Drawing.Point(205, 52);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(161, 21);
            this.labelVersion.TabIndex = 3;
            this.labelVersion.Text = "v.0.0.0000.00000";
            // 
            // labelAuthor
            // 
            this.labelAuthor.Font = new System.Drawing.Font("Microsoft Sans Serif", 2.9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Millimeter, ((byte)(0)));
            this.labelAuthor.Location = new System.Drawing.Point(38, 117);
            this.labelAuthor.Name = "labelAuthor";
            this.labelAuthor.Size = new System.Drawing.Size(353, 31);
            this.labelAuthor.TabIndex = 4;
            this.labelAuthor.Text = "Copyright 2004 - 2015 Riccardo Gerosa";
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 272);
            this.Controls.Add(this.labelLicense);
            this.Controls.Add(this.linkLabel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelAuthor);
            this.Controls.Add(this.labelVersion);
            this.Controls.Add(this.labelStage);
            this.Controls.Add(this.labelKnack);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AboutForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About Knack";
            this.Load += new System.EventHandler(this.AboutFormLoad);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
		void ButtonOkClick(object sender, System.EventArgs e)
		{
			Close();
		}
		
		void LinkLabelLinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start("IExplore","http://code.google.com/p/knack");
			linkLabel.LinkVisited = true;
		}
		
		void AboutFormLoad(object sender, System.EventArgs e)
		{
			this.labelVersion.Text = "v." +
				Assembly.GetExecutingAssembly().GetName().Version.ToString();
		}
		
	}
}
