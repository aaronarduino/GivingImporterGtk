
// This file has been generated by the GUI designer. Do not modify.

public partial class MainWindow
{
	private global::Gtk.HPaned hpaned1;

	private global::Gtk.Table table1;

	private global::Gtk.Table table4;

	private global::Gtk.Fixed fixed1;

	private global::Gtk.Label label3;

	private global::Gtk.VPaned vpaned1;

	private global::Gtk.Table table6;

	private global::Gtk.FileChooserWidget SourceFileChooser;

	private global::Gtk.Table table8;

	private global::Gtk.Fixed fixed3;

	private global::Gtk.Button SelectFileButton;

	private global::Gtk.ScrolledWindow GtkScrolledWindow2;

	private global::Gtk.TextView OutputTextView;

	private global::Gtk.Table table10;

	private global::Gtk.Table table12;

	private global::Gtk.Fixed fixed5;

	private global::Gtk.Label label5;

	private global::Gtk.Table table14;

	private global::Gtk.ScrolledWindow GtkScrolledWindow3;

	private global::Gtk.NodeView QueueNodeView;

	private global::Gtk.Table table16;

	private global::Gtk.Button HideButton;

	private global::Gtk.Button RefreshButton;

	private global::Gtk.Button UploadButton;

	private global::Gtk.ProgressBar UploadProgressBar;

	protected virtual void Build()
	{
		global::Stetic.Gui.Initialize(this);
		// Widget MainWindow
		this.Name = "MainWindow";
		this.Title = global::Mono.Unix.Catalog.GetString("Giving Importer");
		this.WindowPosition = ((global::Gtk.WindowPosition)(4));
		// Container child MainWindow.Gtk.Container+ContainerChild
		this.hpaned1 = new global::Gtk.HPaned();
		this.hpaned1.CanFocus = true;
		this.hpaned1.Name = "hpaned1";
		this.hpaned1.Position = 527;
		this.hpaned1.BorderWidth = ((uint)(10));
		// Container child hpaned1.Gtk.Paned+PanedChild
		this.table1 = new global::Gtk.Table(((uint)(2)), ((uint)(1)), false);
		this.table1.Name = "table1";
		this.table1.RowSpacing = ((uint)(6));
		this.table1.ColumnSpacing = ((uint)(6));
		// Container child table1.Gtk.Table+TableChild
		this.table4 = new global::Gtk.Table(((uint)(1)), ((uint)(2)), false);
		this.table4.Name = "table4";
		this.table4.RowSpacing = ((uint)(6));
		this.table4.ColumnSpacing = ((uint)(6));
		// Container child table4.Gtk.Table+TableChild
		this.fixed1 = new global::Gtk.Fixed();
		this.fixed1.Name = "fixed1";
		this.fixed1.HasWindow = false;
		this.table4.Add(this.fixed1);
		global::Gtk.Table.TableChild w1 = ((global::Gtk.Table.TableChild)(this.table4[this.fixed1]));
		w1.LeftAttach = ((uint)(1));
		w1.RightAttach = ((uint)(2));
		w1.YOptions = ((global::Gtk.AttachOptions)(4));
		// Container child table4.Gtk.Table+TableChild
		this.label3 = new global::Gtk.Label();
		this.label3.Name = "label3";
		this.label3.LabelProp = global::Mono.Unix.Catalog.GetString("1. Select/Split File");
		this.table4.Add(this.label3);
		global::Gtk.Table.TableChild w2 = ((global::Gtk.Table.TableChild)(this.table4[this.label3]));
		w2.XOptions = ((global::Gtk.AttachOptions)(4));
		w2.YOptions = ((global::Gtk.AttachOptions)(4));
		this.table1.Add(this.table4);
		global::Gtk.Table.TableChild w3 = ((global::Gtk.Table.TableChild)(this.table1[this.table4]));
		w3.YOptions = ((global::Gtk.AttachOptions)(4));
		// Container child table1.Gtk.Table+TableChild
		this.vpaned1 = new global::Gtk.VPaned();
		this.vpaned1.CanFocus = true;
		this.vpaned1.Name = "vpaned1";
		this.vpaned1.Position = 365;
		// Container child vpaned1.Gtk.Paned+PanedChild
		this.table6 = new global::Gtk.Table(((uint)(2)), ((uint)(1)), false);
		this.table6.Name = "table6";
		this.table6.RowSpacing = ((uint)(6));
		this.table6.ColumnSpacing = ((uint)(6));
		// Container child table6.Gtk.Table+TableChild
		this.SourceFileChooser = new global::Gtk.FileChooserWidget(((global::Gtk.FileChooserAction)(0)));
		this.SourceFileChooser.Name = "SourceFileChooser";
		this.table6.Add(this.SourceFileChooser);
		// Container child table6.Gtk.Table+TableChild
		this.table8 = new global::Gtk.Table(((uint)(1)), ((uint)(2)), false);
		this.table8.Name = "table8";
		this.table8.RowSpacing = ((uint)(6));
		this.table8.ColumnSpacing = ((uint)(6));
		// Container child table8.Gtk.Table+TableChild
		this.fixed3 = new global::Gtk.Fixed();
		this.fixed3.Name = "fixed3";
		this.fixed3.HasWindow = false;
		this.table8.Add(this.fixed3);
		global::Gtk.Table.TableChild w5 = ((global::Gtk.Table.TableChild)(this.table8[this.fixed3]));
		w5.YOptions = ((global::Gtk.AttachOptions)(4));
		// Container child table8.Gtk.Table+TableChild
		this.SelectFileButton = new global::Gtk.Button();
		this.SelectFileButton.CanFocus = true;
		this.SelectFileButton.Name = "SelectFileButton";
		this.SelectFileButton.UseUnderline = true;
		this.SelectFileButton.Label = global::Mono.Unix.Catalog.GetString("Select File");
		this.table8.Add(this.SelectFileButton);
		global::Gtk.Table.TableChild w6 = ((global::Gtk.Table.TableChild)(this.table8[this.SelectFileButton]));
		w6.LeftAttach = ((uint)(1));
		w6.RightAttach = ((uint)(2));
		w6.XOptions = ((global::Gtk.AttachOptions)(4));
		w6.YOptions = ((global::Gtk.AttachOptions)(4));
		this.table6.Add(this.table8);
		global::Gtk.Table.TableChild w7 = ((global::Gtk.Table.TableChild)(this.table6[this.table8]));
		w7.TopAttach = ((uint)(1));
		w7.BottomAttach = ((uint)(2));
		w7.YOptions = ((global::Gtk.AttachOptions)(4));
		this.vpaned1.Add(this.table6);
		global::Gtk.Paned.PanedChild w8 = ((global::Gtk.Paned.PanedChild)(this.vpaned1[this.table6]));
		w8.Resize = false;
		// Container child vpaned1.Gtk.Paned+PanedChild
		this.GtkScrolledWindow2 = new global::Gtk.ScrolledWindow();
		this.GtkScrolledWindow2.Name = "GtkScrolledWindow2";
		this.GtkScrolledWindow2.ShadowType = ((global::Gtk.ShadowType)(1));
		// Container child GtkScrolledWindow2.Gtk.Container+ContainerChild
		this.OutputTextView = new global::Gtk.TextView();
		this.OutputTextView.CanFocus = true;
		this.OutputTextView.Name = "OutputTextView";
		this.OutputTextView.Editable = false;
		this.GtkScrolledWindow2.Add(this.OutputTextView);
		this.vpaned1.Add(this.GtkScrolledWindow2);
		this.table1.Add(this.vpaned1);
		global::Gtk.Table.TableChild w11 = ((global::Gtk.Table.TableChild)(this.table1[this.vpaned1]));
		w11.TopAttach = ((uint)(1));
		w11.BottomAttach = ((uint)(2));
		this.hpaned1.Add(this.table1);
		global::Gtk.Paned.PanedChild w12 = ((global::Gtk.Paned.PanedChild)(this.hpaned1[this.table1]));
		w12.Resize = false;
		// Container child hpaned1.Gtk.Paned+PanedChild
		this.table10 = new global::Gtk.Table(((uint)(2)), ((uint)(1)), false);
		this.table10.Name = "table10";
		this.table10.RowSpacing = ((uint)(6));
		this.table10.ColumnSpacing = ((uint)(6));
		// Container child table10.Gtk.Table+TableChild
		this.table12 = new global::Gtk.Table(((uint)(1)), ((uint)(2)), false);
		this.table12.Name = "table12";
		this.table12.RowSpacing = ((uint)(6));
		this.table12.ColumnSpacing = ((uint)(6));
		// Container child table12.Gtk.Table+TableChild
		this.fixed5 = new global::Gtk.Fixed();
		this.fixed5.Name = "fixed5";
		this.fixed5.HasWindow = false;
		this.table12.Add(this.fixed5);
		global::Gtk.Table.TableChild w13 = ((global::Gtk.Table.TableChild)(this.table12[this.fixed5]));
		w13.LeftAttach = ((uint)(1));
		w13.RightAttach = ((uint)(2));
		w13.YOptions = ((global::Gtk.AttachOptions)(4));
		// Container child table12.Gtk.Table+TableChild
		this.label5 = new global::Gtk.Label();
		this.label5.Name = "label5";
		this.label5.LabelProp = global::Mono.Unix.Catalog.GetString("2. Upload Files");
		this.table12.Add(this.label5);
		global::Gtk.Table.TableChild w14 = ((global::Gtk.Table.TableChild)(this.table12[this.label5]));
		w14.XOptions = ((global::Gtk.AttachOptions)(4));
		w14.YOptions = ((global::Gtk.AttachOptions)(4));
		this.table10.Add(this.table12);
		global::Gtk.Table.TableChild w15 = ((global::Gtk.Table.TableChild)(this.table10[this.table12]));
		w15.YOptions = ((global::Gtk.AttachOptions)(4));
		// Container child table10.Gtk.Table+TableChild
		this.table14 = new global::Gtk.Table(((uint)(2)), ((uint)(1)), false);
		this.table14.Name = "table14";
		this.table14.RowSpacing = ((uint)(6));
		this.table14.ColumnSpacing = ((uint)(6));
		// Container child table14.Gtk.Table+TableChild
		this.GtkScrolledWindow3 = new global::Gtk.ScrolledWindow();
		this.GtkScrolledWindow3.Name = "GtkScrolledWindow3";
		this.GtkScrolledWindow3.ShadowType = ((global::Gtk.ShadowType)(1));
		// Container child GtkScrolledWindow3.Gtk.Container+ContainerChild
		this.QueueNodeView = new global::Gtk.NodeView();
		this.QueueNodeView.CanFocus = true;
		this.QueueNodeView.Name = "QueueNodeView";
		this.GtkScrolledWindow3.Add(this.QueueNodeView);
		this.table14.Add(this.GtkScrolledWindow3);
		global::Gtk.Table.TableChild w17 = ((global::Gtk.Table.TableChild)(this.table14[this.GtkScrolledWindow3]));
		w17.TopAttach = ((uint)(1));
		w17.BottomAttach = ((uint)(2));
		// Container child table14.Gtk.Table+TableChild
		this.table16 = new global::Gtk.Table(((uint)(1)), ((uint)(4)), false);
		this.table16.Name = "table16";
		this.table16.RowSpacing = ((uint)(6));
		this.table16.ColumnSpacing = ((uint)(6));
		// Container child table16.Gtk.Table+TableChild
		this.HideButton = new global::Gtk.Button();
		this.HideButton.CanFocus = true;
		this.HideButton.Name = "HideButton";
		this.HideButton.UseUnderline = true;
		this.HideButton.Label = global::Mono.Unix.Catalog.GetString("Hide File");
		this.table16.Add(this.HideButton);
		global::Gtk.Table.TableChild w18 = ((global::Gtk.Table.TableChild)(this.table16[this.HideButton]));
		w18.LeftAttach = ((uint)(3));
		w18.RightAttach = ((uint)(4));
		w18.XOptions = ((global::Gtk.AttachOptions)(4));
		w18.YOptions = ((global::Gtk.AttachOptions)(4));
		// Container child table16.Gtk.Table+TableChild
		this.RefreshButton = new global::Gtk.Button();
		this.RefreshButton.CanFocus = true;
		this.RefreshButton.Name = "RefreshButton";
		this.RefreshButton.UseUnderline = true;
		this.RefreshButton.Label = global::Mono.Unix.Catalog.GetString("Refresh");
		this.table16.Add(this.RefreshButton);
		global::Gtk.Table.TableChild w19 = ((global::Gtk.Table.TableChild)(this.table16[this.RefreshButton]));
		w19.LeftAttach = ((uint)(1));
		w19.RightAttach = ((uint)(2));
		w19.XOptions = ((global::Gtk.AttachOptions)(4));
		w19.YOptions = ((global::Gtk.AttachOptions)(4));
		// Container child table16.Gtk.Table+TableChild
		this.UploadButton = new global::Gtk.Button();
		this.UploadButton.CanFocus = true;
		this.UploadButton.Name = "UploadButton";
		this.UploadButton.UseUnderline = true;
		this.UploadButton.Label = global::Mono.Unix.Catalog.GetString("Upload");
		this.table16.Add(this.UploadButton);
		global::Gtk.Table.TableChild w20 = ((global::Gtk.Table.TableChild)(this.table16[this.UploadButton]));
		w20.XOptions = ((global::Gtk.AttachOptions)(4));
		w20.YOptions = ((global::Gtk.AttachOptions)(4));
		// Container child table16.Gtk.Table+TableChild
		this.UploadProgressBar = new global::Gtk.ProgressBar();
		this.UploadProgressBar.Name = "UploadProgressBar";
		this.table16.Add(this.UploadProgressBar);
		global::Gtk.Table.TableChild w21 = ((global::Gtk.Table.TableChild)(this.table16[this.UploadProgressBar]));
		w21.LeftAttach = ((uint)(2));
		w21.RightAttach = ((uint)(3));
		w21.YOptions = ((global::Gtk.AttachOptions)(4));
		this.table14.Add(this.table16);
		global::Gtk.Table.TableChild w22 = ((global::Gtk.Table.TableChild)(this.table14[this.table16]));
		w22.YOptions = ((global::Gtk.AttachOptions)(4));
		this.table10.Add(this.table14);
		global::Gtk.Table.TableChild w23 = ((global::Gtk.Table.TableChild)(this.table10[this.table14]));
		w23.TopAttach = ((uint)(1));
		w23.BottomAttach = ((uint)(2));
		this.hpaned1.Add(this.table10);
		this.Add(this.hpaned1);
		if ((this.Child != null))
		{
			this.Child.ShowAll();
		}
		this.DefaultWidth = 1055;
		this.DefaultHeight = 527;
		this.Show();
		this.DeleteEvent += new global::Gtk.DeleteEventHandler(this.OnDeleteEvent);
		this.SelectFileButton.Clicked += new global::System.EventHandler(this.OnSelectFileButtonClicked);
		this.UploadButton.Clicked += new global::System.EventHandler(this.OnUploadButtonClicked);
		this.RefreshButton.Clicked += new global::System.EventHandler(this.OnRefreshButtonClicked);
		this.HideButton.Clicked += new global::System.EventHandler(this.OnHideButtonClicked);
	}
}