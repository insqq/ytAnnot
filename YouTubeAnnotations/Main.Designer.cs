namespace YouTubeAnnotations
{
    partial class Main
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
            this.cdFirst = new System.Windows.Forms.ColorDialog();
            this.tpTemplates = new System.Windows.Forms.TabPage();
            this.btnStopAction = new System.Windows.Forms.Button();
            this.lblStatusBar = new System.Windows.Forms.Label();
            this.btnDeleteFromSelected = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.tbNewTemaplate = new System.Windows.Forms.TextBox();
            this.pVideo = new System.Windows.Forms.Panel();
            this.pAnnotation = new System.Windows.Forms.Panel();
            this.pbForReplace = new System.Windows.Forms.PictureBox();
            this.lblAnnotationText = new System.Windows.Forms.Label();
            this.picForResize = new System.Windows.Forms.PictureBox();
            this.pAnnotationSettings = new System.Windows.Forms.Panel();
            this.dtpEndTime = new System.Windows.Forms.DateTimePicker();
            this.rtbReference = new System.Windows.Forms.RichTextBox();
            this.dtpStartTime = new System.Windows.Forms.DateTimePicker();
            this.chbReference = new System.Windows.Forms.CheckBox();
            this.cbTextSize = new System.Windows.Forms.ComboBox();
            this.btnForeColorPick = new System.Windows.Forms.Button();
            this.btnTextColorPick = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.rtbAnnotation = new System.Windows.Forms.RichTextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.chbTargetable = new System.Windows.Forms.CheckBox();
            this.cbAnnotationType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAddTemplate = new System.Windows.Forms.Button();
            this.cbAnnotationName = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tpAccount = new System.Windows.Forms.TabPage();
            this.cbSelectAllVideos = new System.Windows.Forms.CheckBox();
            this.lblLoginingStatus = new System.Windows.Forms.Label();
            this.dgvMain = new System.Windows.Forms.DataGridView();
            this.colChecked = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colUrl = new System.Windows.Forms.DataGridViewLinkColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLenght = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colViews = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colComments = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnClearCache = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.btnLogout = new System.Windows.Forms.Button();
            this.tbLogin = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.tbPw = new System.Windows.Forms.TextBox();
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tpNews = new System.Windows.Forms.TabPage();
            this.flpMainContent = new System.Windows.Forms.FlowLayoutPanel();
            this.tpTemplates.SuspendLayout();
            this.pVideo.SuspendLayout();
            this.pAnnotation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbForReplace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picForResize)).BeginInit();
            this.pAnnotationSettings.SuspendLayout();
            this.tpAccount.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).BeginInit();
            this.tcMain.SuspendLayout();
            this.tpNews.SuspendLayout();
            this.SuspendLayout();
            // 
            // tpTemplates
            // 
            this.tpTemplates.Controls.Add(this.btnStopAction);
            this.tpTemplates.Controls.Add(this.lblStatusBar);
            this.tpTemplates.Controls.Add(this.btnDeleteFromSelected);
            this.tpTemplates.Controls.Add(this.btnApply);
            this.tpTemplates.Controls.Add(this.tbNewTemaplate);
            this.tpTemplates.Controls.Add(this.pVideo);
            this.tpTemplates.Controls.Add(this.pAnnotationSettings);
            this.tpTemplates.Controls.Add(this.btnDelete);
            this.tpTemplates.Controls.Add(this.btnEdit);
            this.tpTemplates.Controls.Add(this.btnAddTemplate);
            this.tpTemplates.Controls.Add(this.cbAnnotationName);
            this.tpTemplates.Controls.Add(this.label1);
            this.tpTemplates.Location = new System.Drawing.Point(4, 22);
            this.tpTemplates.Name = "tpTemplates";
            this.tpTemplates.Padding = new System.Windows.Forms.Padding(3);
            this.tpTemplates.Size = new System.Drawing.Size(1056, 569);
            this.tpTemplates.TabIndex = 1;
            this.tpTemplates.Text = "Templates";
            this.tpTemplates.UseVisualStyleBackColor = true;
            // 
            // btnStopAction
            // 
            this.btnStopAction.Location = new System.Drawing.Point(11, 401);
            this.btnStopAction.Name = "btnStopAction";
            this.btnStopAction.Size = new System.Drawing.Size(226, 34);
            this.btnStopAction.TabIndex = 13;
            this.btnStopAction.Text = "Stop current action";
            this.btnStopAction.UseVisualStyleBackColor = true;
            this.btnStopAction.Click += new System.EventHandler(this.btnStopAction_Click);
            // 
            // lblStatusBar
            // 
            this.lblStatusBar.AutoSize = true;
            this.lblStatusBar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblStatusBar.Location = new System.Drawing.Point(11, 463);
            this.lblStatusBar.Name = "lblStatusBar";
            this.lblStatusBar.Size = new System.Drawing.Size(0, 17);
            this.lblStatusBar.TabIndex = 12;
            // 
            // btnDeleteFromSelected
            // 
            this.btnDeleteFromSelected.Location = new System.Drawing.Point(243, 401);
            this.btnDeleteFromSelected.Name = "btnDeleteFromSelected";
            this.btnDeleteFromSelected.Size = new System.Drawing.Size(268, 34);
            this.btnDeleteFromSelected.TabIndex = 11;
            this.btnDeleteFromSelected.Text = "Delete from selected";
            this.btnDeleteFromSelected.UseVisualStyleBackColor = true;
            this.btnDeleteFromSelected.Click += new System.EventHandler(this.btnDeleteFromSelected_Click);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(517, 401);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(268, 34);
            this.btnApply.TabIndex = 10;
            this.btnApply.Text = "Add to selected";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // tbNewTemaplate
            // 
            this.tbNewTemaplate.Location = new System.Drawing.Point(11, 61);
            this.tbNewTemaplate.Name = "tbNewTemaplate";
            this.tbNewTemaplate.Size = new System.Drawing.Size(216, 20);
            this.tbNewTemaplate.TabIndex = 9;
            // 
            // pVideo
            // 
            this.pVideo.BackColor = System.Drawing.Color.LightBlue;
            this.pVideo.Controls.Add(this.pAnnotation);
            this.pVideo.Location = new System.Drawing.Point(11, 95);
            this.pVideo.Name = "pVideo";
            this.pVideo.Size = new System.Drawing.Size(500, 300);
            this.pVideo.TabIndex = 8;
            // 
            // pAnnotation
            // 
            this.pAnnotation.BackColor = System.Drawing.Color.Yellow;
            this.pAnnotation.Controls.Add(this.pbForReplace);
            this.pAnnotation.Controls.Add(this.lblAnnotationText);
            this.pAnnotation.Controls.Add(this.picForResize);
            this.pAnnotation.Location = new System.Drawing.Point(32, 32);
            this.pAnnotation.MaximumSize = new System.Drawing.Size(485, 285);
            this.pAnnotation.MinimumSize = new System.Drawing.Size(30, 30);
            this.pAnnotation.Name = "pAnnotation";
            this.pAnnotation.Size = new System.Drawing.Size(230, 45);
            this.pAnnotation.TabIndex = 0;
            // 
            // pbForReplace
            // 
            this.pbForReplace.BackColor = System.Drawing.Color.Black;
            this.pbForReplace.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.pbForReplace.Location = new System.Drawing.Point(0, 0);
            this.pbForReplace.Name = "pbForReplace";
            this.pbForReplace.Size = new System.Drawing.Size(10, 10);
            this.pbForReplace.TabIndex = 2;
            this.pbForReplace.TabStop = false;
            this.pbForReplace.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbForReplace_MouseDown);
            this.pbForReplace.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbForReplace_MouseMove);
            this.pbForReplace.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbForReplace_MouseUp);
            // 
            // lblAnnotationText
            // 
            this.lblAnnotationText.AutoSize = true;
            this.lblAnnotationText.Location = new System.Drawing.Point(3, 5);
            this.lblAnnotationText.Name = "lblAnnotationText";
            this.lblAnnotationText.Size = new System.Drawing.Size(0, 13);
            this.lblAnnotationText.TabIndex = 1;
            // 
            // picForResize
            // 
            this.picForResize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.picForResize.BackColor = System.Drawing.Color.Black;
            this.picForResize.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.picForResize.Location = new System.Drawing.Point(220, 35);
            this.picForResize.Name = "picForResize";
            this.picForResize.Size = new System.Drawing.Size(10, 10);
            this.picForResize.TabIndex = 0;
            this.picForResize.TabStop = false;
            this.picForResize.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picForResize_MouseDown);
            this.picForResize.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picForResize_MouseMove);
            this.picForResize.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picForResize_MouseUp);
            // 
            // pAnnotationSettings
            // 
            this.pAnnotationSettings.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pAnnotationSettings.Controls.Add(this.dtpEndTime);
            this.pAnnotationSettings.Controls.Add(this.rtbReference);
            this.pAnnotationSettings.Controls.Add(this.dtpStartTime);
            this.pAnnotationSettings.Controls.Add(this.chbReference);
            this.pAnnotationSettings.Controls.Add(this.cbTextSize);
            this.pAnnotationSettings.Controls.Add(this.btnForeColorPick);
            this.pAnnotationSettings.Controls.Add(this.btnTextColorPick);
            this.pAnnotationSettings.Controls.Add(this.label11);
            this.pAnnotationSettings.Controls.Add(this.rtbAnnotation);
            this.pAnnotationSettings.Controls.Add(this.label10);
            this.pAnnotationSettings.Controls.Add(this.label9);
            this.pAnnotationSettings.Controls.Add(this.label8);
            this.pAnnotationSettings.Controls.Add(this.label7);
            this.pAnnotationSettings.Controls.Add(this.label6);
            this.pAnnotationSettings.Controls.Add(this.chbTargetable);
            this.pAnnotationSettings.Controls.Add(this.cbAnnotationType);
            this.pAnnotationSettings.Controls.Add(this.label4);
            this.pAnnotationSettings.Location = new System.Drawing.Point(517, 34);
            this.pAnnotationSettings.Name = "pAnnotationSettings";
            this.pAnnotationSettings.Size = new System.Drawing.Size(268, 361);
            this.pAnnotationSettings.TabIndex = 7;
            // 
            // dtpEndTime
            // 
            this.dtpEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpEndTime.Location = new System.Drawing.Point(193, 106);
            this.dtpEndTime.Name = "dtpEndTime";
            this.dtpEndTime.ShowUpDown = true;
            this.dtpEndTime.Size = new System.Drawing.Size(68, 20);
            this.dtpEndTime.TabIndex = 10;
            // 
            // rtbReference
            // 
            this.rtbReference.Location = new System.Drawing.Point(8, 279);
            this.rtbReference.Name = "rtbReference";
            this.rtbReference.Size = new System.Drawing.Size(253, 74);
            this.rtbReference.TabIndex = 18;
            this.rtbReference.Text = "";
            // 
            // dtpStartTime
            // 
            this.dtpStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpStartTime.Location = new System.Drawing.Point(59, 106);
            this.dtpStartTime.Name = "dtpStartTime";
            this.dtpStartTime.ShowUpDown = true;
            this.dtpStartTime.Size = new System.Drawing.Size(69, 20);
            this.dtpStartTime.TabIndex = 9;
            // 
            // chbReference
            // 
            this.chbReference.AutoSize = true;
            this.chbReference.Location = new System.Drawing.Point(8, 256);
            this.chbReference.Name = "chbReference";
            this.chbReference.Size = new System.Drawing.Size(76, 17);
            this.chbReference.TabIndex = 12;
            this.chbReference.Text = "Reference";
            this.chbReference.UseVisualStyleBackColor = true;
            // 
            // cbTextSize
            // 
            this.cbTextSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTextSize.FormattingEnabled = true;
            this.cbTextSize.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30"});
            this.cbTextSize.Location = new System.Drawing.Point(8, 79);
            this.cbTextSize.Name = "cbTextSize";
            this.cbTextSize.Size = new System.Drawing.Size(50, 21);
            this.cbTextSize.TabIndex = 11;
            this.cbTextSize.SelectedValueChanged += new System.EventHandler(this.cbTextSize_SelectedValueChanged);
            // 
            // btnForeColorPick
            // 
            this.btnForeColorPick.Location = new System.Drawing.Point(167, 77);
            this.btnForeColorPick.Name = "btnForeColorPick";
            this.btnForeColorPick.Size = new System.Drawing.Size(50, 23);
            this.btnForeColorPick.TabIndex = 10;
            this.btnForeColorPick.Text = "pick";
            this.btnForeColorPick.UseVisualStyleBackColor = true;
            this.btnForeColorPick.Click += new System.EventHandler(this.btnForeColorPick_Click);
            // 
            // btnTextColorPick
            // 
            this.btnTextColorPick.Location = new System.Drawing.Point(90, 77);
            this.btnTextColorPick.Name = "btnTextColorPick";
            this.btnTextColorPick.Size = new System.Drawing.Size(50, 23);
            this.btnTextColorPick.TabIndex = 9;
            this.btnTextColorPick.Text = "pick";
            this.btnTextColorPick.UseVisualStyleBackColor = true;
            this.btnTextColorPick.Click += new System.EventHandler(this.btnTextColorPick_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(5, 148);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(85, 13);
            this.label11.TabIndex = 9;
            this.label11.Text = "Annotation Text:";
            // 
            // rtbAnnotation
            // 
            this.rtbAnnotation.Location = new System.Drawing.Point(8, 164);
            this.rtbAnnotation.Name = "rtbAnnotation";
            this.rtbAnnotation.Size = new System.Drawing.Size(254, 82);
            this.rtbAnnotation.TabIndex = 17;
            this.rtbAnnotation.Text = "";
            this.rtbAnnotation.TextChanged += new System.EventHandler(this.rtbAnnotation_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(140, 109);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(51, 13);
            this.label10.TabIndex = 15;
            this.label10.Text = "End time:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(4, 109);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(54, 13);
            this.label9.TabIndex = 9;
            this.label9.Text = "Start time:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(164, 60);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "Back Color:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(87, 60);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Text Color:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 60);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Text Size:";
            // 
            // chbTargetable
            // 
            this.chbTargetable.AutoSize = true;
            this.chbTargetable.Location = new System.Drawing.Point(166, 28);
            this.chbTargetable.Name = "chbTargetable";
            this.chbTargetable.Size = new System.Drawing.Size(77, 17);
            this.chbTargetable.TabIndex = 11;
            this.chbTargetable.Text = "Targetable";
            this.chbTargetable.UseVisualStyleBackColor = true;
            // 
            // cbAnnotationType
            // 
            this.cbAnnotationType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAnnotationType.FormattingEnabled = true;
            this.cbAnnotationType.Items.AddRange(new object[] {
            "popup",
            "anchored"});
            this.cbAnnotationType.Location = new System.Drawing.Point(6, 26);
            this.cbAnnotationType.Name = "cbAnnotationType";
            this.cbAnnotationType.Size = new System.Drawing.Size(121, 21);
            this.cbAnnotationType.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Annotation type: ";
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(233, 34);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(41, 23);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "del";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(417, 34);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(94, 23);
            this.btnEdit.TabIndex = 3;
            this.btnEdit.Text = "save changes";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnAddTemplate
            // 
            this.btnAddTemplate.Location = new System.Drawing.Point(233, 59);
            this.btnAddTemplate.Name = "btnAddTemplate";
            this.btnAddTemplate.Size = new System.Drawing.Size(41, 23);
            this.btnAddTemplate.TabIndex = 2;
            this.btnAddTemplate.Text = "add";
            this.btnAddTemplate.UseVisualStyleBackColor = true;
            this.btnAddTemplate.Click += new System.EventHandler(this.btnAddTemplate_Click);
            // 
            // cbAnnotationName
            // 
            this.cbAnnotationName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAnnotationName.FormattingEnabled = true;
            this.cbAnnotationName.Location = new System.Drawing.Point(11, 34);
            this.cbAnnotationName.Name = "cbAnnotationName";
            this.cbAnnotationName.Size = new System.Drawing.Size(216, 21);
            this.cbAnnotationName.TabIndex = 1;
            this.cbAnnotationName.SelectedIndexChanged += new System.EventHandler(this.cbAnnotationName_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Templates: ";
            // 
            // tpAccount
            // 
            this.tpAccount.Controls.Add(this.cbSelectAllVideos);
            this.tpAccount.Controls.Add(this.lblLoginingStatus);
            this.tpAccount.Controls.Add(this.dgvMain);
            this.tpAccount.Controls.Add(this.btnClearCache);
            this.tpAccount.Controls.Add(this.lblStatus);
            this.tpAccount.Controls.Add(this.label2);
            this.tpAccount.Controls.Add(this.lblName);
            this.tpAccount.Controls.Add(this.btnLogout);
            this.tpAccount.Controls.Add(this.tbLogin);
            this.tpAccount.Controls.Add(this.btnLogin);
            this.tpAccount.Controls.Add(this.tbPw);
            this.tpAccount.Location = new System.Drawing.Point(4, 22);
            this.tpAccount.Name = "tpAccount";
            this.tpAccount.Padding = new System.Windows.Forms.Padding(3);
            this.tpAccount.Size = new System.Drawing.Size(1056, 569);
            this.tpAccount.TabIndex = 0;
            this.tpAccount.Text = "Account";
            this.tpAccount.UseVisualStyleBackColor = true;
            // 
            // cbSelectAllVideos
            // 
            this.cbSelectAllVideos.AutoSize = true;
            this.cbSelectAllVideos.Location = new System.Drawing.Point(216, 59);
            this.cbSelectAllVideos.Name = "cbSelectAllVideos";
            this.cbSelectAllVideos.Size = new System.Drawing.Size(105, 17);
            this.cbSelectAllVideos.TabIndex = 18;
            this.cbSelectAllVideos.Text = "Select All Videos";
            this.cbSelectAllVideos.UseVisualStyleBackColor = true;
            // 
            // lblLoginingStatus
            // 
            this.lblLoginingStatus.AutoSize = true;
            this.lblLoginingStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblLoginingStatus.Location = new System.Drawing.Point(469, 11);
            this.lblLoginingStatus.Name = "lblLoginingStatus";
            this.lblLoginingStatus.Size = new System.Drawing.Size(0, 17);
            this.lblLoginingStatus.TabIndex = 17;
            // 
            // dgvMain
            // 
            this.dgvMain.AllowUserToAddRows = false;
            this.dgvMain.AllowUserToDeleteRows = false;
            this.dgvMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMain.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colChecked,
            this.colUrl,
            this.colName,
            this.colLenght,
            this.colViews,
            this.colComments});
            this.dgvMain.Location = new System.Drawing.Point(9, 85);
            this.dgvMain.Name = "dgvMain";
            this.dgvMain.Size = new System.Drawing.Size(1039, 476);
            this.dgvMain.TabIndex = 16;
            this.dgvMain.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMain_CellContentClick);
            // 
            // colChecked
            // 
            this.colChecked.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colChecked.HeaderText = "✓";
            this.colChecked.Name = "colChecked";
            this.colChecked.Width = 25;
            // 
            // colUrl
            // 
            this.colUrl.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colUrl.HeaderText = "Url";
            this.colUrl.Name = "colUrl";
            this.colUrl.ReadOnly = true;
            this.colUrl.Width = 26;
            // 
            // colName
            // 
            this.colName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colName.HeaderText = "Name";
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            this.colName.Width = 60;
            // 
            // colLenght
            // 
            this.colLenght.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colLenght.HeaderText = "Lenght";
            this.colLenght.Name = "colLenght";
            this.colLenght.ReadOnly = true;
            this.colLenght.Width = 65;
            // 
            // colViews
            // 
            this.colViews.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colViews.HeaderText = "Views";
            this.colViews.Name = "colViews";
            this.colViews.ReadOnly = true;
            this.colViews.Width = 60;
            // 
            // colComments
            // 
            this.colComments.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colComments.HeaderText = "Comments";
            this.colComments.Name = "colComments";
            this.colComments.ReadOnly = true;
            this.colComments.Width = 81;
            // 
            // btnClearCache
            // 
            this.btnClearCache.Location = new System.Drawing.Point(549, 35);
            this.btnClearCache.Name = "btnClearCache";
            this.btnClearCache.Size = new System.Drawing.Size(78, 23);
            this.btnClearCache.TabIndex = 14;
            this.btnClearCache.Text = "clear cache";
            this.btnClearCache.UseVisualStyleBackColor = true;
            this.btnClearCache.Click += new System.EventHandler(this.btnClearCache_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblStatus.Location = new System.Drawing.Point(179, 3);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 22);
            this.lblStatus.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Password";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(24, 6);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(32, 13);
            this.lblName.TabIndex = 9;
            this.lblName.Text = "Email";
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(549, 6);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(78, 23);
            this.btnLogout.TabIndex = 8;
            this.btnLogout.Text = "logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // tbLogin
            // 
            this.tbLogin.Location = new System.Drawing.Point(62, 3);
            this.tbLogin.Name = "tbLogin";
            this.tbLogin.Size = new System.Drawing.Size(100, 20);
            this.tbLogin.TabIndex = 6;
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(6, 55);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(156, 23);
            this.btnLogin.TabIndex = 5;
            this.btnLogin.Text = "login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // tbPw
            // 
            this.tbPw.Location = new System.Drawing.Point(62, 29);
            this.tbPw.Name = "tbPw";
            this.tbPw.Size = new System.Drawing.Size(100, 20);
            this.tbPw.TabIndex = 7;
            this.tbPw.UseSystemPasswordChar = true;
            // 
            // tcMain
            // 
            this.tcMain.Controls.Add(this.tpAccount);
            this.tcMain.Controls.Add(this.tpTemplates);
            this.tcMain.Controls.Add(this.tpNews);
            this.tcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMain.Location = new System.Drawing.Point(0, 0);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(1064, 595);
            this.tcMain.TabIndex = 5;
            // 
            // tpNews
            // 
            this.tpNews.Controls.Add(this.flpMainContent);
            this.tpNews.Location = new System.Drawing.Point(4, 22);
            this.tpNews.Name = "tpNews";
            this.tpNews.Size = new System.Drawing.Size(1056, 569);
            this.tpNews.TabIndex = 2;
            this.tpNews.Text = "News";
            this.tpNews.UseVisualStyleBackColor = true;
            this.tpNews.Enter += new System.EventHandler(this.tpNews_Enter);
            // 
            // flpMainContent
            // 
            this.flpMainContent.AutoScroll = true;
            this.flpMainContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpMainContent.Location = new System.Drawing.Point(0, 0);
            this.flpMainContent.Name = "flpMainContent";
            this.flpMainContent.Size = new System.Drawing.Size(1056, 569);
            this.flpMainContent.TabIndex = 0;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 595);
            this.Controls.Add(this.tcMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Main";
            this.Text = "ytAnnotations";
            this.tpTemplates.ResumeLayout(false);
            this.tpTemplates.PerformLayout();
            this.pVideo.ResumeLayout(false);
            this.pAnnotation.ResumeLayout(false);
            this.pAnnotation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbForReplace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picForResize)).EndInit();
            this.pAnnotationSettings.ResumeLayout(false);
            this.pAnnotationSettings.PerformLayout();
            this.tpAccount.ResumeLayout(false);
            this.tpAccount.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).EndInit();
            this.tcMain.ResumeLayout(false);
            this.tpNews.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ColorDialog cdFirst;
        private System.Windows.Forms.TabPage tpTemplates;
        private System.Windows.Forms.Button btnDeleteFromSelected;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.TextBox tbNewTemaplate;
        private System.Windows.Forms.Panel pVideo;
        private System.Windows.Forms.Panel pAnnotation;
        private System.Windows.Forms.PictureBox pbForReplace;
        private System.Windows.Forms.Label lblAnnotationText;
        private System.Windows.Forms.PictureBox picForResize;
        private System.Windows.Forms.Panel pAnnotationSettings;
        private System.Windows.Forms.DateTimePicker dtpEndTime;
        private System.Windows.Forms.RichTextBox rtbReference;
        private System.Windows.Forms.DateTimePicker dtpStartTime;
        private System.Windows.Forms.CheckBox chbReference;
        private System.Windows.Forms.ComboBox cbTextSize;
        private System.Windows.Forms.Button btnForeColorPick;
        private System.Windows.Forms.Button btnTextColorPick;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.RichTextBox rtbAnnotation;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chbTargetable;
        private System.Windows.Forms.ComboBox cbAnnotationType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAddTemplate;
        private System.Windows.Forms.ComboBox cbAnnotationName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tpAccount;
        private System.Windows.Forms.DataGridView dgvMain;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colChecked;
        private System.Windows.Forms.DataGridViewLinkColumn colUrl;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLenght;
        private System.Windows.Forms.DataGridViewTextBoxColumn colViews;
        private System.Windows.Forms.DataGridViewTextBoxColumn colComments;
        private System.Windows.Forms.Button btnClearCache;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.TextBox tbLogin;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.TextBox tbPw;
        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.Label lblStatusBar;
        private System.Windows.Forms.Label lblLoginingStatus;
        private System.Windows.Forms.Button btnStopAction;
        private System.Windows.Forms.CheckBox cbSelectAllVideos;
        private System.Windows.Forms.TabPage tpNews;
        private System.Windows.Forms.FlowLayoutPanel flpMainContent;



    }
}

