using System.Web;
using System.Web.Optimization;

namespace TBS.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/angularjs").Include(
                    "~/assets/plugins/jquery-1.10.2.min.js",
                    "~/Scripts/angular.js",
                    "~/Scripts/angular-route.js",
                    "~/Scripts/angular-resource.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                         "~/assets/plugins/jquery-1.10.2.min.js"
                        
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));


            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            bundles.Add(new StyleBundle("~/Content/assetscss").Include(
                //"~/assets/css/googleapis.font.css",
                    "~/assets/plugins/font-awesome/css/font-awesome.min.css",
                    "~/assets/plugins/bootstrap/css/bootstrap.min.css",
                    "~/assets/plugins/uniform/css/uniform.default.css",
                    "~/assets/plugins/gritter/css/jquery.gritter.css",
                    "~/assets/plugins/bootstrap-daterangepicker/daterangepicker-bs3.css",
                    "~/assets/plugins/fullcalendar/fullcalendar/fullcalendar.css",
                    "~/assets/plugins/jqvmap/jqvmap/jqvmap.css",
                    "~/assets/plugins/jquery-easy-pie-chart/jquery.easy-pie-chart.css",
                    "~/assets/css/style-metronic.css",
                    "~/assets/css/style.css",
                    "~/assets/css/style-responsive.css",
                    "~/assets/css/plugins.css",
                    "~/assets/css/pages/tasks.css",
                    "~/assets/css/themes/default.css",
                    "~/assets/css/print.css",
                    "~/assets/css/custom.css",
                    "~/assets/plugins/select2/select2.css",
                    "~/assets/plugins/select2/select2-metronic.css",
                    "~/assets/plugins/data-tables/DT_bootstrap.css", "~/Content/ng-table.css"));

            #region Assets JS
            bundles.Add(new ScriptBundle("~/bundles/paging-table").Include(
                  "~/assets/plugins/select2/select2.js",
                 "~/assets/plugins/data-tables/jquery.dataTables.js",
                 "~/assets/plugins/data-tables/DT_bootstrap.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/assetsjs").Include(
            "~/assets/plugins/jquery-1.10.2.min.js",
            "~/assets/plugins/jquery-migrate-1.2.1.min.js",
            "~/assets/plugins/jquery-ui/jquery-ui-1.10.3.custom.min.js",
            "~/assets/plugins/bootstrap/js/bootstrap.min.js",
            "~/assets/plugins/bootstrap-hover-dropdown/bootstrap-hover-dropdown.min.js",
            "~/assets/plugins/jquery-slimscroll/jquery.slimscroll.min.js",
            "~/assets/plugins/jquery.blockui.min.js",
            "~/assets/plugins/jquery.cokie.min.js",
            "~/assets/plugins/uniform/jquery.uniform.min.js",
            "~/assets/plugins/jqvmap/jqvmap/jquery.vmap.js",
            "~/assets/plugins/jqvmap/jqvmap/maps/jquery.vmap.russia.js",
            "~/assets/plugins/jqvmap/jqvmap/maps/jquery.vmap.world.js",
            "~/assets/plugins/jqvmap/jqvmap/maps/jquery.vmap.europe.js",
            "~/assets/plugins/jqvmap/jqvmap/maps/jquery.vmap.germany.js",
            "~/assets/plugins/jqvmap/jqvmap/maps/jquery.vmap.usa.js",
            "~/assets/plugins/jqvmap/jqvmap/data/jquery.vmap.sampledata.js",
            "~/assets/plugins/flot/jquery.flot.min.js",
            "~/assets/plugins/flot/jquery.flot.resize.min.js",
            "~/assets/plugins/flot/jquery.flot.categories.min.js",
            "~/assets/plugins/jquery.pulsate.min.js",
            "~/assets/plugins/bootstrap-daterangepicker/moment.min.js",
            "~/assets/plugins/bootstrap-daterangepicker/daterangepicker.js",
            "~/assets/plugins/gritter/js/jquery.gritter.js",
            "~/assets/plugins/fullcalendar/fullcalendar/fullcalendar.min.js",
            "~/assets/plugins/jquery-easy-pie-chart/jquery.easy-pie-chart.js",
            "~/assets/plugins/jquery.sparkline.min.js",
            "~/assets/plugins/jquery-inputmask/jquery.inputmask.bundle.min.js",
                //start datetime picker
              "~/assets/plugins/bootstrap-datepicker/js/bootstrap-datepicker.js",
             "~/assets/plugins/bootstrap-timepicker/js/bootstrap-timepicker.min.js",
            "~/assets/plugins/clockface/js/clockface.js",
            "~/assets/plugins/bootstrap-daterangepicker/moment.min.js",
            "~/assets/plugins/bootstrap-daterangepicker/daterangepicker.js",
            "~/assets/plugins/bootstrap-colorpicker/js/bootstrap-colorpicker.js",
            "~/assets/plugins/bootstrap-datetimepicker/js/bootstrap-datetimepicker.min.js",
                //end datetimepicker
            "~/assets/scripts/core/app.js",
            "~/assets/scripts/custom/index.js",
            "~/assets/scripts/custom/tasks.js",
            "~/Scripts/ui-bootstrap-tpls-0.10.0.min.js",
                // Table paing
                "~/assets/plugins/select2/select2.js",
                 "~/assets/plugins/data-tables/jquery.dataTables.js",
                 "~/assets/plugins/data-tables/DT_bootstrap.js"
                // ,     "~/assets/scripts/custom/table-advanced.js"
                //Timeout and Idle 
                , "~/Scripts/angular-idle.js"
            ));

            #endregion

            bundles.Add(new ScriptBundle("~/bundles/ng-table").Include(
                "~/Scripts/ng-table.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/nglogincore").Include(
                      "~/Scripts/app/ng-tbs-login-core.js"
                // ,"~/Scripts/app/ng-tbs-services.js"
                      ));
            bundles.Add(new ScriptBundle("~/bundles/ngBootstrapCore").Include(
                       "~/Scripts/app/ng-tbs-core.js",
                       "~/Scripts/app/ng-tbs-services.js", "~/Scripts/app/ng-tbs-services_Dodq.js", "~/Scripts/app/ng-tbs-services_KhoaHT.js", "~/Scripts/app/ng-breadcrumbs.js"
                       ));
            bundles.Add(new ScriptBundle("~/bundles/ngBootstrap").Include(
                        "~/Scripts/app/ng-tbs-core.js",
                        "~/Scripts/app/ng-tbs-services.js", "~/Scripts/app/ng-tbs-services_Dodq.js", "~/Scripts/app/ng-tbs-services_KhoaHT.js"
                        ));


            bundles.Add(new ScriptBundle("~/bundles/uploadfile").Include(
                    "~/Scripts/jupload/jquery.knob.js",
                    "~/Scripts/jupload/jquery.ui.widget.js",
                    "~/Scripts/jupload/jquery.iframe-transport.js",
                    "~/Scripts/jupload/jquery.fileupload.js"));

            #region Master
            bundles.Add(new ScriptBundle("~/bundles/ngMaster").Include(
                        "~/Scripts/app/ng-tbs-master.js"
                        ));
            #endregion

            #region Users
            bundles.Add(new ScriptBundle("~/bundles/ngUsers").Include(
                        "~/Scripts/app/users/ng-user.js"
                        ));



            bundles.Add(new ScriptBundle("~/bundles/userLogin").Include(
                        "~/assets/scripts/custom/login.js"));

            #endregion

            #region Roles
            bundles.Add(new ScriptBundle("~/bundles/ngRoles").Include(
                        "~/Scripts/app/roles/ng-role.js"
                        ));

            #endregion

            #region Bank

            bundles.Add(new ScriptBundle("~/bundles/bankmanagement").Include(
                   "~/Scripts/app/ng-bank-management.js"
                   ));
            #endregion

            #region Charge Back Type
            bundles.Add(new ScriptBundle("~/bundles/ngchargebacktype").Include(
                        "~/Scripts/app/chargebacktype/ng-chargeBackType-create.js",
                        "~/Scripts/app/chargebacktype/ng-chargeBackType-edit.js",
                        "~/Scripts/app/chargebacktype/ng-chargeBackType-list.js"));
            #endregion

            #region Bank

            bundles.Add(new ScriptBundle("~/bundles/ngbank").Include(
                        "~/Scripts/app/bank/ng-bank-list.js",
                        "~/Scripts/app/bank/ng-bank-create.js",
                        "~/Scripts/app/bank/ng-bank-edit.js"
                 ));
            #endregion

            #region Company
            bundles.Add(new ScriptBundle("~/bundles/ngcompany").Include(
                       "~/Scripts/app/company/ng-company-list.js",
                       "~/Scripts/app/company/ng-company-create.js",
                       "~/Scripts/app/company/ng-company-edit.js"
                ));
            #endregion

            #region Model Year Insurance
            bundles.Add(new ScriptBundle("~/bundles/ngmodelyearinsurance").Include(
                        "~/Scripts/app/modelyearinsurance/ng-modelYearInsurance-create.js",
                        "~/Scripts/app/modelyearinsurance/ng-modelYearInsurance-edit.js",
                        "~/Scripts/app/modelyearinsurance/ng-modelYearInsurance-list.js"));
            #endregion

            #region Vehicle Feature
            bundles.Add(new ScriptBundle("~/bundles/ngvehiclefeature").Include(
                        "~/Scripts/app/vehiclefeature/ng-vehicleFeature-create.js",
                        "~/Scripts/app/vehiclefeature/ng-vehicleFeature-edit.js",
                        "~/Scripts/app/vehiclefeature/ng-vehicleFeature-list.js"));
            #endregion

            #region Vehicle Make
            bundles.Add(new ScriptBundle("~/bundles/ngvehiclemake").Include(
                        "~/Scripts/app/vehiclemake/ng-vehicleMake-create.js",
                        "~/Scripts/app/vehiclemake/ng-vehicleMake-edit.js",
                        "~/Scripts/app/vehiclemake/ng-vehicleMake-list.js"));
            #endregion

            #region Vehicle Model
            bundles.Add(new ScriptBundle("~/bundles/ngvehiclemodel").Include(
                        "~/Scripts/app/vehiclemodel/ng-vehicleModel-create.js",
                        "~/Scripts/app/vehiclemodel/ng-vehicleModel-edit.js",
                        "~/Scripts/app/vehiclemodel/ng-vehicleModel-list.js"));
            #endregion

            #region Vender
            bundles.Add(new ScriptBundle("~/bundles/ngvender").Include(
                       "~/Scripts/app/vender/ng-vender-list.js",
                       "~/Scripts/app/vender/ng-vender-create.js",
                       "~/Scripts/app/vender/ng-vender-edit.js"
                ));
            #endregion

            #region WerkShop
            bundles.Add(new ScriptBundle("~/bundles/ngwerkshop").Include(
                      "~/Scripts/app/werkshop/ng-werkshop-list.js",
                      "~/Scripts/app/werkshop/ng-werkshop-create.js",
                      "~/Scripts/app/werkshop/ng-werkshop-edit.js"
               ));
            #endregion

            #region corporateClient
            bundles.Add(new ScriptBundle("~/bundles/ngcorporateclient").Include(
                      "~/Scripts/app/corporateclient/ng-corporateclient-list.js",
                      "~/Scripts/app/corporateclient/ng-corporateclient-create.js",
                      "~/Scripts/app/corporateclient/ng-corporateclient-edit.js"
               ));
            #endregion

            #region Member
            bundles.Add(new ScriptBundle("~/bundles/ngmember").Include(
                      "~/Scripts/app/member/ng-memberInfo-list.js",
                      "~/Scripts/app/member/ng-memberInfo-create.js",
                      "~/Scripts/app/member/ng-memberInfo-edit.js",
                      "~/Scripts/app/member/ng-vehicle.js",
                      "~/Scripts/app/member/ng-searchagent.js"));

            bundles.Add(new ScriptBundle("~/bundles/ngVehicle").Include(
                      "~/Scripts/app/member/ng-vehicle.js"));

            #endregion

            #region TBS Support
            bundles.Add(new ScriptBundle("~/bundles/ngTicket").Include(
                        "~/assets/plugins/ckeditor/ckeditor.js",
                        "~/Scripts/app/support/ng-support-ticket.js"));
            #endregion

            #region Standard Due
            bundles.Add(new ScriptBundle("~/bundles/standarddue").Include(
                        "~/Scripts/app/standarddues/ng-standardDue-create.js",
                        "~/Scripts/app/standarddues/ng-standardDue-edit.js",
                        "~/Scripts/app/standarddues/ng-standardDue-list.js"));
            #endregion

            #region Contact
            bundles.Add(new ScriptBundle("~/bundles/ngcontact").Include(
                       "~/Scripts/app/member/ng-contact-create.js",
                       "~/Scripts/app/member/ng-contact-edit.js",
                       "~/Scripts/app/member/ng-contact-list.js"));
            #endregion

            #region Medallion
            bundles.Add(new ScriptBundle("~/bundles/ngmedallion").Include(
                        "~/Scripts/app/medallion/ng-medallion-create.js",
                        "~/Scripts/app/medallion/ng-medallion-edit.js",
                        "~/Scripts/app/medallion/ng-medallion-list.js"));
            #endregion

            #region Agent
            bundles.Add(new ScriptBundle("~/bundles/ng-agent-search").Include(
               "~/Scripts/app/member/ng-searchagent.js",
               "~/Scripts/app/agents/ng-loanagent.js",
               "~/Scripts/app/agents/ng-accountreceivableagent.js",
               "~/Scripts/app/agents/ng-insurancedepositagent.js",
               "~/Scripts/app/agents/ng-savingdepositagent.js"
               ));

            bundles.Add(new ScriptBundle("~/bundles/ngagent").Include(
                       "~/Scripts/app/member/ng-agent-create.js",
                       "~/Scripts/app/member/ng-agent-edit.js",
                       "~/Scripts/app/member/ng-agent-list.js"));
            #endregion

            #region Cashiering
            bundles.Add(new ScriptBundle("~/bundles/ngcashiering").Include(
                 "~/Scripts/app/cashiering/ng-cashiering-actionmenu.js",
                "~/Scripts/app/cashiering/ng-insurancedeposit.js",
                "~/Scripts/app/cashiering/ng-accountreceivable.js",
                "~/Scripts/app/cashiering/ng-accountsummary.js",
                "~/Scripts/app/cashiering/ng-autoloansetup.js",
                "~/Scripts/app/cashiering/ng-ccsystemairtime.js",
                "~/Scripts/app/cashiering/ng-medallionloansetup.js",
                 "~/Scripts/app/cashiering/ng-savingdeposit.js", 
                 "~/Scripts/app/cashiering/ng-loanlist.js"));
            #endregion

            #region Meter Manufacturer
            bundles.Add(new ScriptBundle("~/bundles/ngmetermanufacturer").Include(
                        "~/Scripts/app/metermanufacturer/ng-meterManufacturer-create.js",
                        "~/Scripts/app/metermanufacturer/ng-meterManufacturer-edit.js",
                        "~/Scripts/app/metermanufacturer/ng-meterManufacturer-list.js"));
            #endregion

            #region Sate
            bundles.Add(new ScriptBundle("~/bundles/ngsate").Include(
                        "~/Scripts/app/sate/ng-sate-create.js",
                        "~/Scripts/app/sate/ng-sate-edit.js",
                        "~/Scripts/app/sate/ng-sate-list.js"));
            #endregion

            #region filter member
            bundles.Add(new ScriptBundle("~/bundles/ngfiltermember").Include(
                        "~/Scripts/app/ng-filtermember.js"));

            #endregion

            #region report
            bundles.Add(new ScriptBundle("~/bundles/ngReport").Include(
                      "~/Scripts/app/report/ng-memberInformationReport.js",
                      "~/Scripts/app/report/ng-PaymentHistoryReport.js",
                      "~/Scripts/app/report/ng-vehicleReport.js",
                      "~/Scripts/app/report/ng-memberContactListReport.js",
                      "~/Scripts/app/report/ng-associationfinancialreport.js"));
            #endregion

            #region UniversalAgent
            bundles.Add(new ScriptBundle("~/bundles/nguniversalagent").Include(
                "~/Scripts/app/agents/ng-agentmodule.js", "~/Scripts/app/agents/ng-rtaupload.js", "~/Scripts/app/agents/ng-mobilitymodule.js"
             ));
            #endregion

            #region Support
            bundles.Add(new ScriptBundle("~/bundles/ngFAQs").Include(
                "~/assets/plugins/jquery-1.10.2.min.js",
                "~/assets/plugins/ckeditor/ckeditor.js",
                "~/Scripts/app/Support/ng-FAQ.js"));

            bundles.Add(new ScriptBundle("~/bundles/ngTutorial").Include(
                "~/assets/plugins/ckeditor/ckeditor.js",
                "~/Scripts/app/Support/ng-Tutorial.js"));
            #endregion


        }
    }
}

