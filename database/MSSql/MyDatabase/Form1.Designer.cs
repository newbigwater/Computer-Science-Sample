
namespace MyDatabase
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.edt_database = new System.Windows.Forms.TextBox();
            this.lb_database = new System.Windows.Forms.Label();
            this.btn_database_isExist = new System.Windows.Forms.Button();
            this.btn_database_create = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // edt_database
            // 
            this.edt_database.Location = new System.Drawing.Point(122, 11);
            this.edt_database.Name = "edt_database";
            this.edt_database.Size = new System.Drawing.Size(200, 28);
            this.edt_database.TabIndex = 0;
            this.edt_database.Text = "MyDatabase";
            this.edt_database.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lb_database
            // 
            this.lb_database.Location = new System.Drawing.Point(12, 9);
            this.lb_database.Name = "lb_database";
            this.lb_database.Size = new System.Drawing.Size(105, 30);
            this.lb_database.TabIndex = 1;
            this.lb_database.Text = "Database : ";
            this.lb_database.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btn_database_isExist
            // 
            this.btn_database_isExist.Location = new System.Drawing.Point(332, 11);
            this.btn_database_isExist.Name = "btn_database_isExist";
            this.btn_database_isExist.Size = new System.Drawing.Size(100, 30);
            this.btn_database_isExist.TabIndex = 2;
            this.btn_database_isExist.Text = "Is Exist?";
            this.btn_database_isExist.UseVisualStyleBackColor = true;
            this.btn_database_isExist.Click += new System.EventHandler(this.btn_database_isExist_Click);
            // 
            // btn_database_create
            // 
            this.btn_database_create.Location = new System.Drawing.Point(437, 11);
            this.btn_database_create.Name = "btn_database_create";
            this.btn_database_create.Size = new System.Drawing.Size(100, 30);
            this.btn_database_create.TabIndex = 3;
            this.btn_database_create.Text = "Create";
            this.btn_database_create.UseVisualStyleBackColor = true;
            this.btn_database_create.Click += new System.EventHandler(this.btn_database_create_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn_database_create);
            this.Controls.Add(this.btn_database_isExist);
            this.Controls.Add(this.lb_database);
            this.Controls.Add(this.edt_database);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox edt_database;
        private System.Windows.Forms.Label lb_database;
        private System.Windows.Forms.Button btn_database_isExist;
        private System.Windows.Forms.Button btn_database_create;
    }
}

