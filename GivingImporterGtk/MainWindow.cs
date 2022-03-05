using System;
using System.IO;
using System.Threading;
using System.Configuration;
using GivingImporterGtk.ViewModels;
using Gtk;
using HCCInfrastructure.Services;
using HCCInfrastructure.Data;
using HCCInfrastructure.Models;
using System.Linq;
using HCCInfrastructure.Helpers;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;

public partial class MainWindow : Gtk.Window
{
    private readonly Repository Repository;
    private readonly GivingImporterService GivingImporterService;

    private string ClientId => ConfigurationManager.AppSettings["ClientId"];
    private string ClientSecret => ConfigurationManager.AppSettings["ClientSecret"];
    private string ApiUrl => ConfigurationManager.AppSettings["ApiUrl"];
    private string DatabaseFileName => ConfigurationManager.AppSettings["DatabaseFileName"];
    private string DefaultBatchName => ConfigurationManager.AppSettings["DefaultBatchName"];
    private int RowSplitLimit => Convert.ToInt32(ConfigurationManager.AppSettings["RowSplitLimit"]);
    private bool ShowHiddenBatchFiles => Convert.ToBoolean(ConfigurationManager.AppSettings["ShowHiddenBatchFiles"]);
    private string FundMappingData => ConfigurationManager.AppSettings["FundMapping"];

    public MainWindow() : base(Gtk.WindowType.Toplevel)
    {
        Build();

        var fundMapping = JsonConvert.DeserializeObject<Dictionary<string, string>>(FundMappingData);

        Repository = new Repository(DatabaseFileName, txt => WriteToScreen(txt));
        GivingImporterService = new GivingImporterService(ApiUrl, ClientId, ClientSecret, RowSplitLimit, fundMapping, Repository, txt => WriteToScreen(txt), fraction => UpdateUploadProgressBar(fraction));

        GivingImporterService.EnsureWorkingDirectoryExists();

        QueueNodeView.AppendColumn("ID", new Gtk.CellRendererText(), "text", 0);
        QueueNodeView.AppendColumn("Status", new Gtk.CellRendererText(), "text", 1);
        QueueNodeView.AppendColumn("Exists", new Gtk.CellRendererText(), "text", 2);
        QueueNodeView.AppendColumn("File", new Gtk.CellRendererText(), "text", 3);
        QueueNodeView.ShowAll();

        QueueNodeView.NodeStore = new Gtk.NodeStore(typeof(DataFileListNode));

        // Populate UI with most updated data
        GivingImporterService.SyncFileSystemAndBatchFileQueue();
        UpdateQueueNodeView();
        WriteToScreen($"RowSplitLimit={RowSplitLimit}");
    }

    private void WriteToScreen(string data)
    {
        Application.Invoke((send, evnt) =>
        {
            OutputTextView.Buffer.InsertAtCursor(data + Environment.NewLine);
        });
    }

    private void DisplayErrorWindow(string message)
    {
        Application.Invoke((send, evnt) =>
        {
            var d = new Gtk.MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Error, ButtonsType.Ok, message);
            d.Run();
            d.Destroy();
        });
        
    }

    private void UpdateQueueNodeView()
    {
        QueueNodeView.NodeStore.Clear();
        var batchFileQueue = ShowHiddenBatchFiles ?
            Repository.GetBatchFileQueue() : Repository.GetBatchFileQueue().Where(r => r.Status != EnumHelper.BatchFileStatus.Hidden.ToString());
        foreach (var file in batchFileQueue)
        {
            QueueNodeView.NodeStore.AddNode(new DataFileListNode(file.ID, file.Exists, file.Status, file.FileName));
        }
    }

    private void UpdateUploadProgressBar(double fraction)
    {
        Application.Invoke((send, evnt) =>
        {
            UploadProgressBar.Fraction = fraction;
        });
    }

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;
    }

    protected void OnSelectFileButtonClicked(object sender, EventArgs e)
    {
        if (SourceFileChooser.Filename == "")
        {
            WriteToScreen("No file selected. Please select a file.");
            return;
        }
        GivingImporterService.ImportFromServantKeeper(SourceFileChooser.Filename);
        UpdateQueueNodeView();
    }

    protected void OnRefreshButtonClicked(object sender, EventArgs e)
    {
        GivingImporterService.SyncFileSystemAndBatchFileQueue();
        UpdateQueueNodeView();
    }

    protected void OnUploadButtonClicked(object sender, EventArgs e)
    {
        DataFileListNode selectedNode = (DataFileListNode)QueueNodeView.NodeSelection.SelectedNode;
        var batchFileInfo = Repository.GetBatchFileByID(Int32.Parse(selectedNode.FileID));
        Task.Run(() => {
            try
            {
                GivingImporterService.UploadBatchToGiving(batchFileInfo, DefaultBatchName, message => DisplayErrorWindow($"[ERROR : File ID {selectedNode.FileID}] " + message));
            }
            catch (Exception ex)
            {
                WriteToScreen($"[ERROR : File ID {selectedNode.FileID}] unable to upload. Error message was: " + ex.Message);
                Repository.SetBatchFileStatus(batchFileInfo.ID, EnumHelper.BatchFileStatus.Error);
                DisplayErrorWindow($"[ERROR : File ID {selectedNode.FileID}] unable to upload. Error message was: " + ex.Message);
            }
            UpdateQueueNodeView();
        });
        UpdateQueueNodeView();
    }

    protected void OnHideButtonClicked(object sender, EventArgs e)
    {
        DataFileListNode selectedNode = (DataFileListNode)QueueNodeView.NodeSelection.SelectedNode;

        if (Int32.TryParse(selectedNode.FileID, out int id))
        {
            Repository.SetBatchFileStatus(id, EnumHelper.BatchFileStatus.Hidden);
            UpdateQueueNodeView();
        }
        else
        {
            WriteToScreen("[ERROR]: Could not hide batch.");
        }
    }
}
