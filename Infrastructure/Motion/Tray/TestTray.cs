using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Toolkit.Helpers;
using System.IO;
using log4net;
namespace Motion.Tray
{
    public partial class TestTray : Form
    {
        static ILog log = LogManager.GetLogger(typeof(TestTray));
        private readonly Action _Refreshing;
        Tray tray = new Tray();//当前托盘对象
        TrayManagement tp;
        private string _ConfigTrayPath;
        private string CurrentProductType;
        public string ConfigTrayPath
        {
            get
            {
                return string.IsNullOrEmpty(_ConfigTrayPath) ? 
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Tray\\"):_ConfigTrayPath;
            }
            set { _ConfigTrayPath = value; }
        }
        public TestTray()
        {
            //初始化托盘 
            InitializeComponent();
        }
        public TestTray(Action Refreshing = null):this()
        {
            _Refreshing = Refreshing;
        }
        private void RefreshFileList()
        {
            cmbId.Items.Clear();
            FileInfo[] files = new DirectoryInfo(ConfigTrayPath).GetFiles();
            foreach (FileInfo file in files)
            {
                cmbId.Items.Add(Path.GetFileNameWithoutExtension(file.Name));
            }
        }
        private void RefreshInfo()
        {
            txtCurrentType.Text = CurrentProductType;
        }
        public void TestTray_Load(object sender, EventArgs e)
        {
            RefreshFileList();
            var count = cmbId.Items.Count;
            cmbStart.SelectedIndex = 0;
            cmbDirect.SelectedIndex = 0;
            cmbChangeLine.SelectedIndex = 0;
            cbxShape.Items.Add(Shape.Cricle.ToString());
            cbxShape.Items.Add(Shape.Rectange.ToString());
            cbxShape.SelectedIndex = 0;
            tp = new TrayManagement(true);
            tp.Dock = DockStyle.Fill;  
            panel1.Controls.Add(tp);
            if (count > 0)
                cmbId.SelectedIndex = 0;
            else
                return;
            CurrentProductType = cmbId.Text;
            tray.Data = SerializerManager<TrayType>.Instance.Load(string.Format("{0}{1}.xml",ConfigTrayPath,CurrentProductType));
            tray.updateColor += tp.UpdateColor;
            tray.SortTray();
            tp.SetTrayObj(tray);
            initControls();
            RefreshInfo();
            chShow.Checked = false;
        }

        private void chxDeletePoint_Click(object sender, EventArgs e)
        {
            tp.IsAddSelect = chxAddPoint.Checked = false;
            tp.IsDelectSelect = chxDeletePoint.Checked;
        }

        private void chxAddPoint_Click(object sender, EventArgs e)
        {
            tp.IsDelectSelect = chxDeletePoint.Checked=false;
            tp.IsAddSelect = chxAddPoint.Checked;
        }
        //修改设置
        private void btnModify_Click(object sender, EventArgs e)
        {
            log.Info("用户点击进行机种新增操作！");
            if (tray==null) return;
            chShow.Checked = false;
            tray.Data.Type = cmbId.Text;
            tray.Data.Name = txtName.Text;
            tray.Data.Row = (int)nudRow.Value;
            tray.Data.Column = (int)nudCol.Value;
            tray.SortTray();
            tp.SetTrayObj(tray);
            tray.ResetTrayColor(Color.Gray);
            RefreshInfo();
        }

        private void chShow_CheckedChanged(object sender, EventArgs e)
        {
            if (tray == null)
            {
                MessageBox.Show("当前盘为空，请先创建托盘！");
                return;
            }
            tp.IsShowModel = chShow.Checked;
            tray.Data.StartPose = (EStartPos)Enum.Parse(typeof(EStartPos), cmbStart.Text);
            tray.Data.Direction = (EIndexDirect)Enum.Parse(typeof(EIndexDirect), cmbDirect.Text);
            tray.SortTray();
            if(!tp.IsShowModel) tray.ResetTrayColor(Color.Gray);
            else tray.ResetTrayColor(Color.LightSteelBlue);
            panel3.Enabled = chShow.Checked;
        }

        private void chxReguar1_Click(object sender, EventArgs e)
        {
            if (!chShow.Checked)
            {
                chxReguar1.Checked = false;
                return;
            }
            for (int i = 0; i < tray.Data.Row; i++)
                for (int j = 0; j < tray.Data.Column; j++)
                    tray.RemoveEmptyPos(i, j);
            tray.SortTray();
            chxReguar2.Checked = false;
            for (int i = 0; i < tray.Data.Row; i++)
            {
                for (int j = 0; j < tray.Data.Column; j++)
                {
                    if (chxReguar1.Checked)
                    {
                        if (i % 2 == 0)
                        {
                            if (j % 2 == 0) tray.AddEmptyPos(i, j);
                            else tray.RemoveEmptyPos(i, j);
                        }
                        else
                        {
                            if (j % 2 == 1) tray.AddEmptyPos(i, j);
                            else tray.RemoveEmptyPos(i, j);
                        }
                    }
                    else tray.RemoveEmptyPos(i, j);
                }
            }
            tray.ResetTrayColor(Color.LightSteelBlue);
        }

        private void chxReguar2_Click(object sender, EventArgs e)
        {
            if (!chShow.Checked)
            {
                chxReguar2.Checked = false;
                return;
            }
            for (int i = 0; i < tray.Data.Row; i++)
                for (int j = 0; j < tray.Data.Column; j++)
                    tray.RemoveEmptyPos(i, j);
            tray.SortTray();
            chxReguar1.Checked = false;
            for (int i = 0; i < tray.Data.Row; i++)
            {
                for (int j = 0; j < tray.Data.Column; j++)
                {
                    if (chxReguar2.Checked)
                    {
                        if (i % 2 == 0)
                        {
                            if (j % 2 == 1) tray.AddEmptyPos(i, j);
                            else tray.RemoveEmptyPos(i, j);
                        }
                        else
                        {
                            if (j % 2 == 0) tray.AddEmptyPos(i, j);
                            else tray.RemoveEmptyPos(i, j);
                        }
                    }
                    else tray.RemoveEmptyPos(i, j);
                }
            }
            tray.ResetTrayColor(Color.LightSteelBlue);
        }
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            tp.IsShowModel = chShow.Checked = false;
            tray.Data.IsShowID = chxIsShowID.Checked;
            tray.Data.StartPose = (EStartPos)Enum.Parse(typeof(EStartPos), cmbStart.Text);
            tray.Data.Direction = (EIndexDirect)Enum.Parse(typeof(EIndexDirect), cmbDirect.Text);
            tray.Data.ChangeLineType = (EChangeLine)Enum.Parse(typeof(EChangeLine), cmbChangeLine.Text);
            tray.Data.Shape = (Shape)cbxShape.SelectedIndex;
            tray.SortTray();
            tp.SetTrayObj(tray);
            tray.ResetTrayColor(Color.Gray);
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            log.Info("用户点击进行机种切换操作！");
            var cout = cmbId.Items.Count;
            if (cout <= 0) return;
            string selectType = cmbId.Text.Trim();
            if (string.IsNullOrEmpty(selectType))
            {
                MessageBox.Show("目标型号值为空，请确认是否选中对应型号！");
                return;
            }
            if (selectType == CurrentProductType)
            {
                MessageBox.Show("目标型号与正在使用的型号一直，无需切换！");
                return;
            }
            CurrentProductType = selectType;
            tray.Data = SerializerManager<TrayType>.Instance.Load(string.Format("{0}{1}.xml",ConfigTrayPath,CurrentProductType));
            tray.SortTray();
            tp.IsShowModel = chShow.Checked =false;
            tp.SetTrayObj(tray);
            tray.ResetTrayColor(Color.Gray);
            initControls();
            RefreshInfo();
        }
        //根据选择的托盘对象初始化值
        private void initControls()
        {
            if (tray != null)
            {
                try
                {
                    txtName.Text = tray.Data.Name;
                    nudRow.Value = tray.Data.Row;
                    nudCol.Value = tray.Data.Column;
                    cmbStart.Text = tray.Data.StartPose.ToString();
                    cmbDirect.Text = tray.Data.Direction.ToString();
                    cmbChangeLine.Text = tray.Data.ChangeLineType.ToString();
                    cbxShape.SelectedIndex = (int)tray.Data.Shape;
                    chxIsShowID.Checked = tray.Data.IsShowID;
                    ndnRowDistance.Value = (decimal)tray.Data.RowDistance;
                    ndnColDistance.Value = (decimal)tray.Data.ColDistance;
                    ndnRowColoffset.Value = (decimal)tray.Data.RowColOffset;
                    ndnColRowoffset.Value = (decimal)tray.Data.ColRowOffset;
                    ndnFinalBaseIndex.Value = (decimal)tray.Data.BaseIndex ;
                    ndnFinalRowIndex.Value = (decimal)tray.Data.RowIndex;
                    ndnFinalColumnIndex.Value = (decimal)tray.Data.ColumnIndex;
                }
                catch(Exception ex) { }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(string.Format("是否保存型号 {0} 的数据？并确认行列序号正确", CurrentProductType), "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                SaveParameter();
                SerializerManager<TrayType>.Instance.Save(string.Format("{0}{1}.xml", ConfigTrayPath, CurrentProductType), tray.Data);
                tray.Data.IsCalibration = false;
            }
            if (_Refreshing != null) _Refreshing();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            log.Info("用户点击进行机种删除操作！");
            string deleteType = cmbId.Text.Trim();
            if (string.IsNullOrEmpty(deleteType))
            {
                MessageBox.Show("目标型号不能为空！");
                return;
            }

            if (deleteType == CurrentProductType)
            {
                MessageBox.Show("目标型号正在使用，不能删除！");
                return;
            }

            if (!cmbId.Items.Contains(deleteType))
            {
                MessageBox.Show("列表中未找到目标，无法删除！");
                return;
            }
            if (MessageBox.Show(string.Format("是否删除型号：{0}", deleteType), "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                FileInfo[] files = new DirectoryInfo(ConfigTrayPath).GetFiles();
                foreach (FileInfo file in files)
                {
                    if (Path.GetFileNameWithoutExtension(file.Name) == deleteType)
                    {
                        file.Delete();
                        break;
                    }
                }
                RefreshFileList();
            }
        }
        private bool IsDigitOrNumber(string str) {return !System.Text.RegularExpressions.Regex.IsMatch(str, @"(?i)^[0-9a-zA-Z]+$");}

        #region 标定位置
        private void SaveParameter()
        {
            tray.Data.BaseIndex = (int)ndnFinalBaseIndex.Value;

            tray.Data.RowIndex =  (int)ndnFinalRowIndex.Value;

            tray.Data.ColumnIndex = (int)ndnFinalColumnIndex.Value;

            tray.Data.RowColOffset = (double)ndnRowColoffset.Value;

            tray.Data.RowDistance = (double)ndnRowDistance.Value;

            tray.Data.ColDistance = (double)ndnColDistance.Value;

            tray.Data.ColRowOffset = (double)ndnColRowoffset.Value;
        }
        #endregion

        private void btnAdd_Click(object sender, EventArgs e)
        {
            log.Info("用户点击进行机种新增操作！");
            string newType = txtNewPlateType.Text.Trim();
            if (string.IsNullOrEmpty(newType) || IsDigitOrNumber(newType))
            {
                MessageBox.Show("目标型号不能为空,或非法字符！");
                return;
            }
            if (cmbId.Items.Contains(newType))
            {
                MessageBox.Show("列表中已有相同型号，不能再次新增！");
                return;
            }
            foreach (char c in Path.GetInvalidFileNameChars())
            {
                if (newType.Contains(c))
                {
                    MessageBox.Show("名称中不能包含特殊字符 '{c}' 请重新命名！");
                    return;
                }
            }
            tray.Data = SerializerManager<TrayType>.Instance.Load(string.Format("{0}{1}.xml", ConfigTrayPath, newType));
            SerializerManager<TrayType>.Instance.Save(string.Format("{0}{1}.xml", ConfigTrayPath, newType), tray.Data);
            RefreshFileList();
        }
    }
}
