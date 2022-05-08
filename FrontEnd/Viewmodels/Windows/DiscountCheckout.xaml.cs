using System;
using System.Windows;
using Hotelmanagement.BackEnd.Models.CustomerDiscounts;
using Hotelmanagement.BackEnd.Models.Visit;
using Hotelmanagement.BackEnd.ViewModels.SpecialOffers;

namespace Hotelmanagement.FrontEnd.Viewmodels.Windows;

public partial class DiscountCheckout : Window
{
    public DiscountCheckout(int visitId)
    {
        VisitId = visitId;
        InitializeComponent();
        UpdateCustomerDiscountComboBox();
        UpdateSpecialOffersComboBox();
        this.visitId.Text = visitId.ToString();
    }
    
    public int VisitId { get; set; }
    
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
                    CustomerDiscountsDB.GetCustomerDiscountsById(Int32.Parse(customerDiscount.ToString() ?? string.Empty));
                customerDiscountValue = customerDiscounts.DiscountValue;
                specialOfferValue = 0;
                //TODO: Add function to update visit database entry
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
                customerDiscountValue = 0;
            }
            else
            {
                MessageBox.Show("Es ist keine Angebotsaktion ausgewählt.");
            }
        }
        
        MessageBox.Show("Success!");
    }
}