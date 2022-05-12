using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using Hotelmanagement.BackEnd.Models.CustomerPaymentmethods;
using Hotelmanagement.BackEnd.Models.Invoice;
using Hotelmanagement.BackEnd.Models.Tax;
using Hotelmanagement.BackEnd.Models.TypesOfStay;
using Hotelmanagement.BackEnd.Models.Visit;
using Hotelmanagement.BackEnd.ViewModels.CustomerPaymentmethods;
using Hotelmanagement.BackEnd.ViewModels.TypesOfStay;

namespace Hotelmanagement.FrontEnd.Viewmodels
{
    public partial class InvoiceTab : UserControl
    {
        public InvoiceTab()
        {
            InitializeComponent();
            UpdateDataGrid();
        }


        private void UpdateDataGrid()
        {
            var dataset = InvoiceDB.DataSetInvoice();
            InvoiceDataGrid.ItemsSource = dataset.Tables["rechnung"]?.DefaultView;
        }

        private void UpdateComboBox(int id)
        {
            var dataset = CustomerPaymentMethodsDB.GetDataSetCustomerPayMeth(id);
            if (dataset != null)
            {
                PaymentmethodsComboBox.ItemsSource = dataset.Tables[0].DefaultView;
            }
            
        }
        private void InvoiceDataGrid_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object item = InvoiceDataGrid.SelectedItem;
            
            if (item != null)
            {
                var invoiceId = (InvoiceDataGrid.SelectedCells[0].Column.GetCellContent(item) as TextBlock)?.Text;
                var visitId = (InvoiceDataGrid.SelectedCells[1].Column.GetCellContent(item) as TextBlock)?.Text;
                var roomName = (InvoiceDataGrid.SelectedCells[3].Column.GetCellContent(item) as TextBlock)?.Text; 
                
                if (visitId == "")
                {
                    MessageBox.Show("Es existiert kein Besuch zu dieser Rechnung");
                    return;
                }

                if (visitId != null)
                {
                    Visit visit = VisitDB.GetVisitById(Int32.Parse(visitId));
                    hiddenInvoiceId.Text = invoiceId;
                    hiddenRoomName.Text = roomName;
                    UpdateComboBox(visit.Customer_ID);
                }
            }
        }

        private void ShowInvoiceDisplay(object sender, RoutedEventArgs e)
        {
            if (PaymentmethodsComboBox.SelectedItem != null && hiddenInvoiceId.Text != "" && hiddenRoomName.Text != "")
            {
                GenerateInvoice(Int32.Parse(hiddenInvoiceId.Text));
            }
            
        }

        private void GenerateInvoice(int invoiceId)
        {
            Invoice invoice = InvoiceDB.GetInvoiceById(invoiceId);                                      // Invoice object
            int visitId = invoice.VisitID;                                                              // ID of the visit
            Visit visit = VisitDB.GetVisitById(visitId);                                                // Visit object
            TypesOfStay typesOfStay = TypesOfStayDB.GetTypeOfStayByID(visit.Visit_Type_Of_Stay_ID);     // Type of stay object
            
            RoomText.Text = hiddenRoomName.Text;                                                        // Room name
            double roomCosts = Math.Round(invoice.Room_Costs, 2);                                       // Costs for the room
            RoomPriceText.Text = roomCosts.ToString(CultureInfo.InvariantCulture) + "€";                // Room costs
            double serviceCosts = Math.Round(invoice.Service_Costs, 2);
            ServicePriceText.Text = serviceCosts.ToString(CultureInfo.InvariantCulture) + "€";          // Costs for the services 
            double restaurantCosts = Math.Round(invoice.Restaurant_Costs, 2);
            RestaurantPriceText.Text = restaurantCosts.ToString(CultureInfo.InvariantCulture) + "€";    // Costs for restaurant

            string typeOfStayName = typesOfStay.Bezeichnung;                                            // Type of stay name
            double typeOfStayCosts = Math.Round(typesOfStay.Price_change, 2);                           // Costs for the type of stay
            
            //Get the discount
            double customerDiscount = invoice.Customer_Discount_Value;                                  // Value of the customer discount
            double specialOfferDiscount = invoice.Specialoffers_Value;                                  // Value of the special offer discount
            double discount = 0;

            //Check which discount the customer gets
            if (specialOfferDiscount != 0)
            {
                discount = Math.Round(specialOfferDiscount, 2);
                DiscountHeader.Text = "Angebotsaktion:";
            }
            else if(customerDiscount != 0)
            {
                discount = Math.Round(customerDiscount, 2);
                DiscountHeader.Text = "Kundenrabatt:";
            }
            else
            {
                DiscountHeader.Text = "Rabatt";
                discount = 0;
            }
            
            DiscountText.Text = discount.ToString(CultureInfo.InvariantCulture) + "€";
            
            //Tax calculation
            double visitTotal = invoice.VisitTotalCosts;                                        // Total Costs incl. tax
            visitTotal = Math.Round(visitTotal, 2);
            Tax tax = TaxDB.GetTaxById(invoice.Tax_Id);                                         // Tax object
            TaxesHeader.Text = tax.Designation;                                                 // Designation of the taxes
            double grossSum = visitTotal - invoice.Tax_Sum;                                     // Costs without taxes
            grossSum = Math.Round(grossSum, 2);
            GrossPriceText.Text = grossSum.ToString(CultureInfo.InvariantCulture) + "€";        // Gross price text
            double taxSum = Math.Round(invoice.Tax_Sum,2);
            TaxesText.Text = taxSum.ToString(CultureInfo.InvariantCulture) + "€";               // tax sum text
            double total = visitTotal - discount;
            total = Math.Round(total, 2);
            TotalText.Text = total.ToString(CultureInfo.InvariantCulture) + "€";                 // Total costs text

            TypeOfStayNameText.Text = typeOfStayName;                                            // Type of stay name
            TypeOfStayPriceText.Text = typeOfStayCosts.ToString(CultureInfo.InvariantCulture) + "€"; // Type of stay price text
            
            string arrival = visit.Arrival.ToShortDateString();
            string departure = visit.Departure.ToShortDateString();
            
            ArrivalText.Text = arrival;                                                         // Arrival text
            DepartureText.Text = departure;                                                     // Departure text
            InvoiceDB.BookInvoice(invoiceId);                                                   // complete the invoice
            UpdateDataGrid();
        }

        private void Refresh(object sender, RoutedEventArgs e)
        {
            UpdateDataGrid();
        }
    }
}