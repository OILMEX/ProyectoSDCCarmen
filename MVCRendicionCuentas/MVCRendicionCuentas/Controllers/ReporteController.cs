using RendicionCuentasServices.Services;
using RendicionCuentasServices.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Utils;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using System.Diagnostics;
using System.Drawing;

namespace MVCRendicionCuentas.Controllers
{
    public class ReporteController : ValidatorController
    {
        srvReporte srv = new srvReporte();

        // GET: Reporte
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ExportReport()
        {

            if (myCookie != null)
            {
                var model = srv.GetAllData();
                try
                {
                    return GridViewExtension.ExportToXlsx(GridViewHelper.ExportGridViewSettings, model, new XlsxExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
                }
                catch (TypeAccessException ex)
                {
                    Trace.Write(ex.Message);
                }

                return null;
            }
            else
            {
                return RedireccionaLogin();
            }

        }
        

        public ActionResult ReporteGeneral(string est = null)
        {

            if (myCookie != null)
            {

                //var model = (Rol == "Administrador") ? srv.GetDataGeneral() : srv.GetDataGeneral(IdUbicacion);
                var model = (Rol == "Administrador") ? (est != null ? srv.GetDataGeneral(0, est) : srv.GetDataGeneral(0, "En Proceso")) : (est != null ? srv.GetDataGeneral(IdUbicacion, est) : srv.GetDataGeneral(IdUbicacion, "En Proceso"));

                
                return PartialView("ReporteGeneral", model);
            }
            else
            {
                return RedireccionaLogin();
            }

        }

        public ActionResult ExportarDatosPartial()
        {
            var model = (Rol == "Administrador") ? srv.GetDataGeneral() : srv.GetDataGeneral(IdUbicacion);
            return PartialView(model);
        }

        public ActionResult ExportToDataGeneral(string OutputFormat, string est = null)
        {
            string[] param=null;
            if (est != null)
            {
                param = est.Split('?');

            }



            var model = (Rol == "Administrador") ? (est != null ? srv.GetDataGeneral(0, param[0]) : srv.GetDataGeneral(0, "En Proceso")) : (est != null ? srv.GetDataGeneral(IdUbicacion, param[0]) : srv.GetDataGeneral(IdUbicacion, "En Proceso"));
                //model = model.Where(x => x.EstatusTiempo == param[0]).ToList();
                

            
           

            return GridViewExtension.ExportToXlsx(GridViewHelper2.ExportGridViewSettings, model, new XlsxExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });

        }
    }

    //clase PAra Manejar Grid Reporte Empleado
    public static class GridViewHelper
    {
        private static GridViewSettings exportGridViewSettings;

        public static GridViewSettings ExportGridViewSettings
        {
            get
            {
                if (exportGridViewSettings == null)
                    exportGridViewSettings = CreateExportGridViewSettings();
                return exportGridViewSettings;
            }
        }

        private static GridViewSettings CreateExportGridViewSettings()
        {
            GridViewSettings settings = new GridViewSettings();

            settings.Name = "ExportProyectos";
            settings.KeyFieldName = "IdProyecto";
            settings.SettingsText.Title = "Reporte de Proyectos";
            //settings.CallbackRouteValues = new { Controller = "Reporte", Action = "ExportarEmpleadosPartial" };

            settings.Settings.ShowTitlePanel = true;
            settings.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
            settings.SettingsEditing.Mode = GridViewEditingMode.PopupEditForm;
            settings.Theme = "Metropolisblue";

            settings.Settings.ShowHeaderFilterButton = false;
            settings.SettingsPopup.HeaderFilter.Height = 300;
            settings.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
            settings.Settings.ShowFilterRowMenu = true;
            settings.Settings.ShowFooter = true;
            settings.Settings.ShowGroupButtons = true;
            settings.Settings.ShowGroupFooter = GridViewGroupFooterMode.VisibleIfExpanded;

            settings.CommandColumn.Visible = false;
            settings.CommandColumn.Caption = " ";

            settings.Width = System.Web.UI.WebControls.Unit.Percentage(95);

            settings.Settings.UseFixedTableLayout = true;
            settings.SettingsBehavior.ColumnResizeMode = ColumnResizeMode.NextColumn;

            settings.Settings.ShowGroupPanel = true;
            settings.Settings.ShowFilterRow = true;
            settings.SettingsBehavior.AllowSelectByRowClick = true;

            settings.Columns.Add(col =>
            {
                col.FieldName = "IdProyecto";
                col.Caption = "IdProyecto";
                //col.Width = Unit.Percentage(15);
            });

            settings.Columns.Add(col =>
            {
                col.FieldName = "Prefijo";
                col.Caption = "Prefijo";
                col.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                col.HeaderStyle.Wrap = DefaultBoolean.True;
                col.SortOrder = 0;
            });

            settings.Columns.Add(col =>
            {
                col.FieldName = "DescripcionCorta";
                col.Caption = "Descripción";
                col.HeaderStyle.Wrap = DefaultBoolean.True;
                col.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                col.SortOrder = 0;
            });

            settings.Columns.Add(col =>
            {
                col.FieldName = "EstatusLlenado";
                col.Caption = "Estatus";
                col.HeaderStyle.Wrap = DefaultBoolean.True;
                col.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                col.SortOrder = 0;
            });

            settings.Columns.Add(col =>
            {
                col.FieldName = "TipoIndicador";
                col.Caption = "Tipo Indicador";
                col.HeaderStyle.Wrap = DefaultBoolean.True;
                col.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                col.SortOrder = 0;
            });
            settings.Columns.Add(col =>
            {
                col.FieldName = "TipoFrecuencia";
                col.Caption = "Tipo Frecuencia";
                col.HeaderStyle.Wrap = DefaultBoolean.True;
                col.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                col.SortOrder = 0;
            });

            settings.Columns.Add(col =>
            {
                col.FieldName = "TipoCalculo";
                col.Caption = "Tipo de Calculo";
                col.HeaderStyle.Wrap = DefaultBoolean.True;
                col.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                col.SortOrder = 0;
            });

            settings.Columns.Add(col =>
            {
                col.FieldName = "Meta";
                col.Caption = "Meta";
                col.HeaderStyle.Wrap = DefaultBoolean.True;
                col.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                col.SortOrder = 0;
            });

            settings.Columns.Add(col =>
            {
                col.FieldName = "Valor";
                col.Caption = "Valor";
                col.HeaderStyle.Wrap = DefaultBoolean.True;
                col.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                col.SortOrder = 0;
            });

            settings.Columns.Add(col =>
            {
                col.FieldName = "ClaveEtapa";
                col.Caption = "Clave Etapa";
                col.HeaderStyle.Wrap = DefaultBoolean.True;
                col.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                col.SortOrder = 0;
            });

            settings.Columns.Add(col =>
            {
                col.FieldName = "NombreEtapa";
                col.Caption = "Nombre Etapa";
                col.HeaderStyle.Wrap = DefaultBoolean.True;
                col.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                col.SortOrder = 0;
            });

            settings.Columns.Add(col =>
            {
                col.FieldName = "ClaveSubEtapa";
                col.Caption = "Clave Subetapa";
                col.HeaderStyle.Wrap = DefaultBoolean.True;
                col.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                col.SortOrder = 0;
            });

            settings.Columns.Add(col =>
            {
                col.FieldName = "NombreSubEtapa";
                col.Caption = "Nombre de Subetapa";
                col.HeaderStyle.Wrap = DefaultBoolean.True;
                col.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                col.SortOrder = 0;
            });

            settings.Columns.Add(col =>
            {
                col.FieldName = "NombreSubsistema";
                col.Caption = "Nombre Subsistema";
                col.HeaderStyle.Wrap = DefaultBoolean.True;
                col.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                col.SortOrder = 0;
            });

            settings.Columns.Add(col =>
            {
                col.FieldName = "NombreElemento";
                col.Caption = "Nombre de Elemento";
                col.HeaderStyle.Wrap = DefaultBoolean.True;
                col.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                col.SortOrder = 0;
            });

            settings.Columns.Add(col =>
            {
                col.FieldName = "DescripcionElemento";
                col.Caption = "Descripción Elemento";
                col.HeaderStyle.Wrap = DefaultBoolean.True;
                col.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                col.SortOrder = 0;
            });


            settings.Columns.Add(col =>
            {
                col.FieldName = "FechaFinEtapa";
                col.Caption = "F. Fin Etapa";
                col.HeaderStyle.Wrap = DefaultBoolean.True;
            });

            settings.Columns.Add(col =>
            {
                col.FieldName = "FechaFinSubEtapa";
                col.Caption = "F. Fin Subetapa";
                col.HeaderStyle.Wrap = DefaultBoolean.True;
            });

            settings.Columns.Add(col =>
            {
                col.FieldName = "IdProyecto";
                col.Caption = "IdProyecto";
                col.HeaderStyle.Wrap = DefaultBoolean.True;
                col.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                col.SortOrder = 0;
            });


            settings.Columns.Add(col =>
            {
                col.FieldName = "NombreProyecto";
                col.Caption = "Nombre del Proyecto";
                col.HeaderStyle.Wrap = DefaultBoolean.True;
                col.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                col.SortOrder = 0;
            });

            settings.Columns.Add(col =>
            {
                col.FieldName = "NombreResponsable";
                col.Caption = "Responsable";
                col.HeaderStyle.Wrap = DefaultBoolean.True;
                col.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                col.SortOrder = 0;
            });

            settings.CommandColumn.VisibleIndex = settings.Columns.Count;

            settings.CustomJSProperties = (sender, e) =>
            {
                e.Properties["cpFilterExpression"] = (sender as MVCxGridView).FilterExpression;
            };

            settings.ClientSideEvents.EndCallback = "OnEndCallback2";
            settings.SettingsExport.ReportHeader = "Relación Proyecto-Responsable-Indicador SDC" + "\r\n" + "Fecha Emisión: " + DateTime.Now.ToString() + "\r\n" + "Fuente: " + "Sistema de Rendición de Cuentas";
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            return settings;
        }
    }


        //Method Static for Data General
        public static class GridViewHelper2
        {
            private static GridViewSettings exportGridViewSettings;

            public static GridViewSettings ExportGridViewSettings
            {
                get
                {
                    if (exportGridViewSettings == null)
                        exportGridViewSettings = CreateExportGridViewSettings2();
                    return exportGridViewSettings;
                }
            }

            private static GridViewSettings CreateExportGridViewSettings2()
            {
                GridViewSettings settings = new GridViewSettings();

                settings.Name = "ExportarDataGeneral";
                settings.KeyFieldName = "IdConfigIndicadorResponsable";
                settings.SettingsText.Title = "Reporte General";
                settings.CallbackRouteValues = new { Controller = "Reporte", Action = "ExportarDatosPartial" };

                settings.Settings.ShowTitlePanel = true;
                settings.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
                settings.SettingsEditing.Mode = GridViewEditingMode.PopupEditForm;
                settings.Theme = "Metropolis";

                settings.Settings.ShowHeaderFilterButton = false;
                settings.SettingsPopup.HeaderFilter.Height = 300;
                settings.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                settings.Settings.ShowFilterRowMenu = true;
                settings.Settings.ShowFooter = true;
                settings.Settings.ShowGroupButtons = true;
                settings.Settings.ShowGroupFooter = GridViewGroupFooterMode.VisibleIfExpanded;

                settings.CommandColumn.Visible = false;
                settings.CommandColumn.Caption = " ";

                settings.Width = System.Web.UI.WebControls.Unit.Percentage(100);

                settings.Settings.UseFixedTableLayout = true;
                settings.SettingsBehavior.ColumnResizeMode = ColumnResizeMode.NextColumn;

                settings.Settings.ShowGroupPanel = true;
                settings.Settings.ShowFilterRow = true;
                settings.SettingsBehavior.AllowSelectByRowClick = true;

                settings.Columns.Add(col =>
                {
                    col.FieldName = "#";
                    col.Caption = "#";
                    col.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
                    col.EditFormSettings.VisibleIndex = 0;
                    col.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left;
                    col.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left;
                    col.Width = Unit.Percentage(3);
                });
                settings.Columns.Add(col =>
                {
                    col.FieldName = "Ubicacion";
                    col.Caption = "Gerencia";
                    col.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                    col.HeaderStyle.Wrap = DefaultBoolean.True;
                    //col.Width = Unit.Percentage(15);
                    col.SortOrder = 0;
                });
                settings.Columns.Add(col =>
                {
                    col.FieldName = "NombreProyecto";
                    col.Caption = "Proyecto";
                    col.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                    col.HeaderStyle.Wrap = DefaultBoolean.True;
                    //col.Width = Unit.Percentage(15);
                    col.SortOrder = 0;
                });
                settings.Columns.Add(col =>
                {
                    col.FieldName = "NombreResponsable";
                    col.Caption = "Responsable";
                    col.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                    col.HeaderStyle.Wrap = DefaultBoolean.True;
                    //col.Width = Unit.Percentage(15);
                    col.SortOrder = 0;
                });
                settings.Columns.Add(col =>
                {
                    col.FieldName = "ClaveEtapa";
                    col.Caption = "Etapa";
                    col.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                    col.HeaderStyle.Wrap = DefaultBoolean.True;
                    //col.Width = Unit.Percentage(15);
                    col.SortOrder = 0;
                });
                settings.Columns.Add(col =>
                {
                    col.FieldName = "ClaveSubEtapa";
                    col.Caption = "SubEtapa";
                    col.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                    col.HeaderStyle.Wrap = DefaultBoolean.True;
                    //col.Width = Unit.Percentage(15);
                    col.SortOrder = 0;
                });
                settings.Columns.Add(col =>
                {
                    col.FieldName = "Prefijo";
                    col.Caption = "Indicador";
                    col.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                    col.HeaderStyle.Wrap = DefaultBoolean.True;
                    //col.Width = Unit.Percentage(15);
                    col.SortOrder = 0;
                });
                settings.Columns.Add(col =>
                {
                    col.FieldName = "DescripcionCorta";
                    col.Caption = "Descripción de Indicador";
                    col.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                    col.HeaderStyle.Wrap = DefaultBoolean.True;
                    //col.Width = Unit.Percentage(15);
                    col.SortOrder = 0;
                });

                settings.Columns.Add(col =>
                {
                    col.FieldName = "Valor";
                    col.Caption = "Avance[%]";
                    col.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                    col.HeaderStyle.Wrap = DefaultBoolean.True;
                    //col.Width = Unit.Percentage(15);
                    col.SortOrder = 0;
                });

                settings.Columns.Add(col =>
                {
                    col.FieldName = "Semaforo";
                    col.Caption = "Estatus";
                    col.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                    col.HeaderStyle.Wrap = DefaultBoolean.True;
                    //col.Width = Unit.Percentage(15);
                    col.SortOrder = 0;
                });

                settings.FormatConditions.AddHighlight("Semaforo", "[Valor] >= 81 AND [Valor] < 96", GridConditionHighlightFormat.YellowFillWithDarkYellowText);
                settings.FormatConditions.AddHighlight("Semaforo", "[Valor] >= 96", GridConditionHighlightFormat.GreenFillWithDarkGreenText);
                settings.FormatConditions.AddHighlight("Semaforo", "[Valor] < 81", GridConditionHighlightFormat.LightRedFillWithDarkRedText);
                //settings.CommandColumn.VisibleIndex = settings.Columns.Count;

                settings.CustomColumnDisplayText = (sender, e) =>
                {
                    if (e.Column.FieldName == "#")
                    {
                        e.DisplayText = (e.VisibleRowIndex + 1).ToString();
                    }
                };
                settings.CustomJSProperties = (sender, e) =>
                {
                    e.Properties["cpFilterExpression"] = (sender as MVCxGridView).FilterExpression;
                };

                settings.ClientSideEvents.EndCallback = "OnEndCallback2";
                settings.SettingsExport.ReportHeader = "Título: Reportes General" + "\r\n" + "Fecha Emisión: " + DateTime.Now.ToString() + "\r\n" + "Fuente: " + "App Tableros SDC";
                settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
                return settings;
            }
        }


}