
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
            this.btn_table_create = new System.Windows.Forms.Button();
            this.btn_table_isExist = new System.Windows.Forms.Button();
            this.lb_table = new System.Windows.Forms.Label();
            this.edt_table = new System.Windows.Forms.TextBox();
            this.btn_database_drop = new System.Windows.Forms.Button();
            this.btn_table_drop = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // edt_database
            // 
            this.edt_database.Location = new System.Drawing.Point(98, 9);
            this.edt_database.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.edt_database.Name = "edt_database";
            this.edt_database.Size = new System.Drawing.Size(161, 25);
            this.edt_database.TabIndex = 0;
            this.edt_database.Text = "MyDatabase";
            this.edt_database.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lb_database
            // 
            this.lb_database.Location = new System.Drawing.Point(10, 7);
            this.lb_database.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lb_database.Name = "lb_database";
            this.lb_database.Size = new System.Drawing.Size(84, 25);
            this.lb_database.TabIndex = 1;
            this.lb_database.Text = "Database : ";
            this.lb_database.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btn_database_isExist
            // 
            this.btn_database_isExist.Location = new System.Drawing.Point(266, 9);
            this.btn_database_isExist.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_database_isExist.Name = "btn_database_isExist";
            this.btn_database_isExist.Size = new System.Drawing.Size(80, 25);
            this.btn_database_isExist.TabIndex = 2;
            this.btn_database_isExist.Text = "Is Exist?";
            this.btn_database_isExist.UseVisualStyleBackColor = true;
            this.btn_database_isExist.Click += new System.EventHandler(this.btn_database_isExist_Click);
            // 
            // btn_database_create
            // 
            this.btn_database_create.Location = new System.Drawing.Point(350, 9);
            this.btn_database_create.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_database_create.Name = "btn_database_create";
            this.btn_database_create.Size = new System.Drawing.Size(80, 25);
            this.btn_database_create.TabIndex = 3;
            this.btn_database_create.Text = "Create";
            this.btn_database_create.UseVisualStyleBackColor = true;
            this.btn_database_create.Click += new System.EventHandler(this.btn_database_create_Click);
            // 
            // btn_table_create
            // 
            this.btn_table_create.Location = new System.Drawing.Point(350, 48);
            this.btn_table_create.Margin = new System.Windows.Forms.Padding(2);
            this.btn_table_create.Name = "btn_table_create";
            this.btn_table_create.Size = new System.Drawing.Size(80, 25);
            this.btn_table_create.TabIndex = 7;
            this.btn_table_create.Text = "Create";
            this.btn_table_create.UseVisualStyleBackColor = true;
            this.btn_table_create.Click += new System.EventHandler(this.btn_table_create_Click);
            // 
            // btn_table_isExist
            // 
            this.btn_table_isExist.Location = new System.Drawing.Point(266, 48);
            this.btn_table_isExist.Margin = new System.Windows.Forms.Padding(2);
            this.btn_table_isExist.Name = "btn_table_isExist";
            this.btn_table_isExist.Size = new System.Drawing.Size(80, 25);
            this.btn_table_isExist.TabIndex = 6;
            this.btn_table_isExist.Text = "Is Exist?";
            this.btn_table_isExist.UseVisualStyleBackColor = true;
            this.btn_table_isExist.Click += new System.EventHandler(this.btn_table_isExist_Click);
            // 
            // lb_table
            // 
            this.lb_table.Location = new System.Drawing.Point(10, 46);
            this.lb_table.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lb_table.Name = "lb_table";
            this.lb_table.Size = new System.Drawing.Size(84, 25);
            this.lb_table.TabIndex = 5;
            this.lb_table.Text = "Table : ";
            this.lb_table.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // edt_table
            // 
            this.edt_table.Location = new System.Drawing.Point(98, 48);
            this.edt_table.Margin = new System.Windows.Forms.Padding(2);
            this.edt_table.Name = "edt_table";
            this.edt_table.Size = new System.Drawing.Size(161, 25);
            this.edt_table.TabIndex = 4;
            this.edt_table.Text = "MyTable";
            this.edt_table.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btn_database_drop
            // 
            this.btn_database_drop.Location = new System.Drawing.Point(434, 10);
            this.btn_database_drop.Margin = new System.Windows.Forms.Padding(2);
            this.btn_database_drop.Name = "btn_database_drop";
            this.btn_database_drop.Size = new System.Drawing.Size(80, 25);
            this.btn_database_drop.TabIndex = 8;
            this.btn_database_drop.Text = "Drop";
            this.btn_database_drop.UseVisualStyleBackColor = true;
            this.btn_database_drop.Click += new System.EventHandler(this.btn_database_drop_Click);
            // 
            // btn_table_drop
            // 
            this.btn_table_drop.Location = new System.Drawing.Point(434, 48);
            this.btn_table_drop.Margin = new System.Windows.Forms.Padding(2);
            this.btn_table_drop.Name = "btn_table_drop";
            this.btn_table_drop.Size = new System.Drawing.Size(80, 25);
            this.btn_table_drop.TabIndex = 9;
            this.btn_table_drop.Text = "Drop";
            this.btn_table_drop.UseVisualStyleBackColor = true;
            this.btn_table_drop.Click += new System.EventHandler(this.btn_table_drop_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 375);
            this.Controls.Add(this.btn_table_drop);
            this.Controls.Add(this.btn_database_drop);
            this.Controls.Add(this.btn_table_create);
            this.Controls.Add(this.btn_table_isExist);
            this.Controls.Add(this.lb_table);
            this.Controls.Add(this.edt_table);
            this.Controls.Add(this.btn_database_create);
            this.Controls.Add(this.btn_database_isExist);
            this.Controls.Add(this.lb_database);
            this.Controls.Add(this.edt_database);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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
        private System.Windows.Forms.Button btn_table_create;
        private System.Windows.Forms.Button btn_table_isExist;
        private System.Windows.Forms.Label lb_table;
        private System.Windows.Forms.TextBox edt_table;
        private System.Windows.Forms.Button btn_database_drop;
        private System.Windows.Forms.Button btn_table_drop;
    }
}

