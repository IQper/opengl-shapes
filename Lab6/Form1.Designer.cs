namespace Lab6
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.openglControl1 = new SharpGL.OpenGLControl();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxLighting = new System.Windows.Forms.CheckBox();
            this.checkBoxNormals = new System.Windows.Forms.CheckBox();
            this.checkBoxOutline = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.openglControl1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // openglControl1
            // 
            this.openglControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.openglControl1.DrawFPS = true;
            this.openglControl1.FrameRate = 60;
            this.openglControl1.Location = new System.Drawing.Point(3, 19);
            this.openglControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.openglControl1.Name = "openglControl1";
            this.openglControl1.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            this.openglControl1.RenderContextType = SharpGL.RenderContextType.DIBSection;
            this.openglControl1.RenderTrigger = SharpGL.RenderTrigger.TimerBased;
            this.openglControl1.Size = new System.Drawing.Size(1068, 767);
            this.openglControl1.TabIndex = 0;
            this.openglControl1.OpenGLDraw += new SharpGL.RenderEventHandler(this.openglControl1_OpenGLDraw);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.textBox1.Location = new System.Drawing.Point(3, 19);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(292, 731);
            this.textBox1.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxLighting);
            this.groupBox1.Controls.Add(this.checkBoxNormals);
            this.groupBox1.Controls.Add(this.checkBoxOutline);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox1.Location = new System.Drawing.Point(1080, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(298, 789);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Log";
            // 
            // checkBoxLighting
            // 
            this.checkBoxLighting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.checkBoxLighting.AutoSize = true;
            this.checkBoxLighting.Location = new System.Drawing.Point(7, 90);
            this.checkBoxLighting.Name = "checkBoxLighting";
            this.checkBoxLighting.Size = new System.Drawing.Size(70, 19);
            this.checkBoxLighting.TabIndex = 2;
            this.checkBoxLighting.Text = "Lighting";
            this.checkBoxLighting.UseVisualStyleBackColor = true;
            this.checkBoxLighting.CheckedChanged += new System.EventHandler(this.checkBoxOutline_CheckedChanged);
            // 
            // checkBoxNormals
            // 
            this.checkBoxNormals.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.checkBoxNormals.AutoSize = true;
            this.checkBoxNormals.Location = new System.Drawing.Point(6, 40);
            this.checkBoxNormals.Name = "checkBoxNormals";
            this.checkBoxNormals.Size = new System.Drawing.Size(71, 19);
            this.checkBoxNormals.TabIndex = 2;
            this.checkBoxNormals.Text = "Normals";
            this.checkBoxNormals.UseVisualStyleBackColor = true;
            this.checkBoxNormals.CheckedChanged += new System.EventHandler(this.checkBoxOutline_CheckedChanged);
            // 
            // checkBoxOutline
            // 
            this.checkBoxOutline.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.checkBoxOutline.AutoSize = true;
            this.checkBoxOutline.Location = new System.Drawing.Point(6, 65);
            this.checkBoxOutline.Name = "checkBoxOutline";
            this.checkBoxOutline.Size = new System.Drawing.Size(65, 19);
            this.checkBoxOutline.TabIndex = 2;
            this.checkBoxOutline.Text = "Outline";
            this.checkBoxOutline.UseVisualStyleBackColor = true;
            this.checkBoxOutline.CheckedChanged += new System.EventHandler(this.checkBoxOutline_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.openglControl1);
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1074, 789);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "View";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1378, 789);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.openglControl1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private SharpGL.OpenGLControl openglControl1;
        private TextBox textBox1;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private CheckBox checkBoxOutline;
        private CheckBox checkBoxNormals;
        private CheckBox checkBoxLighting;
    }
}