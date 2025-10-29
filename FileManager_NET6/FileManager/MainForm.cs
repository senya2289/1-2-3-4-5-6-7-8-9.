using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace FileManager
{
    public class MainForm : Form
    {
        private SplitContainer split;
        private TreeView tree;
        private ListView list;
        private ToolStrip tool;
        private ToolStripButton btnRefresh, btnNewFolder, btnCopy, btnMove, btnDelete;
        private StatusStrip status;
        private ToolStripStatusLabel statusLabel;

        public MainForm()
        {
            Text = "File Manager (.NET 6)";
            Width = 1000;
            Height = 600;
            StartPosition = FormStartPosition.CenterScreen;

            InitializeComponents();
            Load += (s, e) => PopulateDrives();
        }

        private void InitializeComponents()
        {
            tool = new ToolStrip();
            btnRefresh = new ToolStripButton("Обновить", null, (s, e) => RefreshCurrent());
            btnNewFolder = new ToolStripButton("Новая папка", null, BtnNewFolder_Click);
            btnCopy = new ToolStripButton("Копировать", null, BtnCopy_Click);
            btnMove = new ToolStripButton("Переместить", null, BtnMove_Click);
            btnDelete = new ToolStripButton("Удалить", null, BtnDelete_Click);
            tool.Items.AddRange(new ToolStripItem[] { btnRefresh, btnNewFolder, btnCopy, btnMove, btnDelete });

            split = new SplitContainer { Dock = DockStyle.Fill, SplitterDistance = 300 };
            tree = new TreeView { Dock = DockStyle.Fill };
            tree.BeforeExpand += Tree_BeforeExpand;
            tree.AfterSelect += (s, e) => ShowFiles(e.Node.Tag as string);
            list = new ListView { Dock = DockStyle.Fill, View = View.Details, FullRowSelect = true };
            list.Columns.Add("Имя", 300);
            list.Columns.Add("Тип", 100);
            list.Columns.Add("Размер", 100);
            list.DoubleClick += (s, e) => OpenSelected();

            split.Panel1.Controls.Add(tree);
            split.Panel2.Controls.Add(list);

            status = new StatusStrip();
            statusLabel = new ToolStripStatusLabel("Готово");
            status.Items.Add(statusLabel);

            Controls.Add(split);
            Controls.Add(tool);
            Controls.Add(status);
        }

        private void PopulateDrives()
        {
            tree.Nodes.Clear();
            foreach (var d in DriveInfo.GetDrives().Where(d => d.IsReady))
            {
                var node = new TreeNode(d.Name) { Tag = d.RootDirectory.FullName };
                node.Nodes.Add("...");
                tree.Nodes.Add(node);
            }
        }

        private void Tree_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            e.Node.Nodes.Clear();
            try
            {
                foreach (var dir in Directory.GetDirectories(e.Node.Tag.ToString()))
                {
                    var node = new TreeNode(Path.GetFileName(dir)) { Tag = dir };
                    node.Nodes.Add("...");
                    e.Node.Nodes.Add(node);
                }
            }
            catch { }
        }

        private void ShowFiles(string path)
        {
            list.Items.Clear();
            if (path == null) return;
            try
            {
                foreach (var dir in Directory.GetDirectories(path))
                {
                    var di = new DirectoryInfo(dir);
                    var item = new ListViewItem(di.Name);
                    item.SubItems.Add("Папка");
                    item.SubItems.Add("");
                    item.Tag = di.FullName;
                    list.Items.Add(item);
                }
                foreach (var file in Directory.GetFiles(path))
                {
                    var fi = new FileInfo(file);
                    var item = new ListViewItem(fi.Name);
                    item.SubItems.Add(fi.Extension);
                    item.SubItems.Add(fi.Length.ToString());
                    item.Tag = fi.FullName;
                    list.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                statusLabel.Text = ex.Message;
            }
        }

        private void RefreshCurrent()
        {
            if (tree.SelectedNode != null)
                ShowFiles(tree.SelectedNode.Tag as string);
            else
                PopulateDrives();
        }

        private void BtnNewFolder_Click(object sender, EventArgs e)
        {
            if (tree.SelectedNode == null) return;
            var path = tree.SelectedNode.Tag as string;
            var newDir = Path.Combine(path, "New Folder");
            int i = 1;
            while (Directory.Exists(newDir)) newDir = Path.Combine(path, $"New Folder ({i++})");
            Directory.CreateDirectory(newDir);
            RefreshCurrent();
        }

        private void BtnCopy_Click(object sender, EventArgs e)
        {
            if (list.SelectedItems.Count == 0) return;
            using var fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() != DialogResult.OK) return;
            foreach (ListViewItem item in list.SelectedItems)
            {
                var src = item.Tag.ToString();
                var dest = Path.Combine(fbd.SelectedPath, Path.GetFileName(src));
                if (Directory.Exists(src)) CopyDirectory(src, dest);
                else File.Copy(src, dest, false);
            }
        }

        private void BtnMove_Click(object sender, EventArgs e)
        {
            if (list.SelectedItems.Count == 0) return;
            using var fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() != DialogResult.OK) return;
            foreach (ListViewItem item in list.SelectedItems)
            {
                var src = item.Tag.ToString();
                var dest = Path.Combine(fbd.SelectedPath, Path.GetFileName(src));
                if (Directory.Exists(src)) Directory.Move(src, dest);
                else File.Move(src, dest);
            }
            RefreshCurrent();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (list.SelectedItems.Count == 0) return;
            if (MessageBox.Show("Удалить выбранные элементы?", "Подтверждение", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
            foreach (ListViewItem item in list.SelectedItems)
            {
                var src = item.Tag.ToString();
                if (Directory.Exists(src)) Directory.Delete(src, true);
                else if (File.Exists(src)) File.Delete(src);
            }
            RefreshCurrent();
        }

        private void OpenSelected()
        {
            if (list.SelectedItems.Count == 0) return;
            var tag = list.SelectedItems[0].Tag.ToString();
            if (Directory.Exists(tag))
            {
                foreach (TreeNode node in tree.Nodes)
                    if (node.Tag.ToString() == tag) tree.SelectedNode = node;
            }
            else
                Process.Start(new ProcessStartInfo(tag) { UseShellExecute = true });
        }

        private void CopyDirectory(string sourceDir, string destDir)
        {
            Directory.CreateDirectory(destDir);
            foreach (string file in Directory.GetFiles(sourceDir))
                File.Copy(file, Path.Combine(destDir, Path.GetFileName(file)), false);
            foreach (string dir in Directory.GetDirectories(sourceDir))
                CopyDirectory(dir, Path.Combine(destDir, Path.GetFileName(dir)));
        }
    }
}
