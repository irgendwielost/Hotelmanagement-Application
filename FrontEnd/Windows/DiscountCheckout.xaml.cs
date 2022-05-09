using System;
using System.Windows;
using Hotelmanagement.BackEnd.Models.CustomerDiscounts;
using Hotelmanagement.BackEnd.Models.Invoice;
using Hotelmanagement.BackEnd.Models.Visit;
using Hotelmanagement.BackEnd.ViewModels.SpecialOffers;
using Hotelmanagement.BackEnd.ViewModels.VisitDiscount;

namespace Hotelmanagement.FrontEnd.Windows;

public partial class DiscountCheckout : Window
{
    public DiscountCheckout(int visitId, int invoiceId)
    {
        VisitId = visitId;
        InvoiceId = invoiceId;
        InitializeComponent();
        UpdateCustomerDiscountComboBox();
        UpdateSpecialOffersComboBox();
    }
    
    public int VisitId { get; set; }
    public int InvoiceId { get; set; }
    
    private void UpdateCustomerDiscountComboBox()
    {
        try
        {
            Visit visit = VisitDB.GetVisitById(VisitId);
            if (visit.Customer_ID != null)
            {
                
                var dataset = CustomerDiscountsDB.GetDataSetCustomerDiscountsById(visit.Customer_ID);
                if (dataset.Tables[0].Rows.Count != 0)
                {
                    customerDiscountsComboBox.ItemsSource = dataset.Tables[0].DefaultView;
                }
                else
                {
                    CustomerDiscountRadioButton.IsEnabled = false;
                    customerDiscountsComboBox.SelectedItem = "Keine Kundenrabatte";
                }
            }
        }
        catch (Exception e)
        {
            MessageBox.Show("Daten zu den Kundenrabatten konnten nicht geladen werden.\n" +"Error:"+  e.Message);
        }
    }
    
    private void UpdateSpecialOffersComboBox()
    {
        try
        {
            var dataset = SpecialOffersDB.GetDataSetSpecialOffers();
            SpecialOffersComboBox.ItemsSource = dataset.Tables[0].DefaultView;
        }
        catch (Exception e)
        {
            MessageBox.Show("Daten zu den Angebotsaktionen konnten nicht geladen werden.\n" +"Error:"+  e.Message);
        }
    }

    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        double customerDiscountValue = 0;
        double specialOfferValue = 0;
        
        //Get data to visit
        Visit visit = VisitDB.GetVisitById(VisitId);
        double subtotal = visit.Total_Costs;
        
        if(CustomerDiscountRadioButton.IsChecked == true)
        {
            var customerDiscount = customerDiscountsComboBox.SelectedValue;
            if (customerDiscount != null)
            {
                CustomerDiscounts customerDiscounts = 
                    CustomerDiscountsDB.GetCustomerDiscountsById((int)customerDiscount);
                if (customerDiscounts != null)
                {
                    customerDiscountValue = customerDiscounts.DiscountValue;
                    double discount = subtotal * customerDiscountValue; //calculate the discount €
                    double totalcosts = subtotal - discount; //calculate the total costs minus the discount
                    
                    MessageBox.Show("Total: " + totalcosts + "\n Discount: " + discount);
                    
                    //Update the visit entry for the costs and discount
                    VisitDB.UpdateVisit(new Visit(VisitId, visit.Customer_ID, visit.Visit_Type_Of_Stay_ID, 
                        visit.Room_ID, visit.Person_Amount, visit.Service_Costs, visit.Room_Costs, visit.Arrival, 
                        visit.Departure, totalcosts, visit.Complained, visit.Complain_Reason, visit.Dish_Costs, 
                        true, false, true));
                    
                    //Add the discount to the visit discount table
                    VisitDiscountDB.AddVisitDiscount(new VisitDiscount(VisitId, 
                        (int)customerDiscount, 0, discount));
                    
                    //Update invoice entry
                    Invoice invoice = InvoiceDB.GetInvoiceById(InvoiceId);
                    
                    InvoiceDB.UpdateInvoice(new Invoice(InvoiceId, invoice.VisitID, invoice.VisitTotalCosts,
                        invoice.Room_Costs, invoice.Service_Costs, invoice.Restaurant_Costs,
                        discount, 0, invoice.Tax_Id, invoice.Tax_Sum,
                        invoice.Paid, invoice.Deleted));
                    Close(); //close window
                }
                
            }
            else
            {
                MessageBox.Show("Es ist kein Kundenrabatt ausgewählt.");
            }
            
        }
        else
        {
            var specialOffer = SpecialOffersComboBox.SelectedValue;
            if (specialOffer != null)
            {
                SpecialOffers specialOffers =
                    SpecialOffersDB.GetSpecialOffersById(Int32.Parse(specialOffer.ToString() ?? string.Empty));
                specialOfferValue = specialOffers.Rabatt;
                
                double discount = subtotal * specialOfferValue; // calculate the discount €
                double totalcosts = subtotal - discount; //calculate the total costs minus the discount
                    
                MessageBox.Show("Total: " + totalcosts + "\n Discount: " + discount);
                    
                //Update the visit entry for the costs and discount
                VisitDB.UpdateVisit(new Visit(VisitId, visit.Customer_ID, visit.Visit_Type_Of_Stay_ID, 
                    visit.Room_ID, visit.Person_Amount, visit.Service_Costs, visit.Room_Costs, visit.Arrival, 
                    visit.Departure, totalcosts, visit.Complained, visit.Complain_Reason, visit.Dish_Costs, 
                    false, true, true));
                
                //Add the discount to the visit discount table
                VisitDiscountDB.AddVisitDiscount(new VisitDiscount(VisitId, 
                    0, (int)specialOffer, discount)); 
                
                //Update invoice entry
                Invoice invoice = InvoiceDB.GetInvoiceById(InvoiceId);
                    
                InvoiceDB.UpdateInvoice(new Invoice(InvoiceId, invoice.VisitID, invoice.VisitTotalCosts,
                    invoice.Room_Costs, invoice.Service_Costs, invoice.Restaurant_Costs,
                    0, discount, invoice.Tax_Id, invoice.Tax_Sum,
                    invoice.Paid, invoice.Deleted));
                
                Close(); //close window
            }
            else
            {
                MessageBox.Show("Es ist keine Angebotsaktion ausgewählt.");
            }
        }
    }
}