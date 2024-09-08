using Store.Enums;
using Store.Models;
using Store.Services.Interfaces;

namespace Store.Services;

public class CartService : ICartService
{
    public Task<bool> CreateCart(Cart cart, out string cartErrorMessage) 
    {
        if (cart.Number > 0)
            if (CheckGoodInfo(cart, out string goodInfoErrorMessage, out decimal goodsTotal))
            {
                if (CheckPaymentInfo(cart, goodsTotal, out string paymentInfoErrorMessage)) 
                {
                    cartErrorMessage = string.Empty;
                    return Task.FromResult(true);
                }
                else 
                {
                    cartErrorMessage = paymentInfoErrorMessage;
                    return Task.FromResult(false);
                }
            }
            else
            {
                cartErrorMessage = goodInfoErrorMessage;
                return Task.FromResult(false);
            }
        else 
        {
            cartErrorMessage = "Number must be greater than 0!";
            return Task.FromResult(false);
        }
    }

    private bool CheckGoodInfo(Cart cart, out string goodInfoErrorMessage, out decimal goodsTotal) 
    {
        // I don't know how to do without it 😁
        var _goodsTotal = 0.0m;

        if (cart.Goods.Any())
        {
            foreach (var good in cart.Goods)
            {
                // Soliq requirements 🤬
                if (good.Code!.Count() != 13) throw new Exception($"{good.Code} is not valid as barcode!");
                if (good.MXIK!.Count() != 17) throw new Exception($"{good.MXIK} is not valid as MXIK!");
                if (good.UnitCode < 100000)   throw new Exception($"{good.UnitCode} is not valid as UnitCode!");

                // Price is not less than Discount, otherwise price like this -10...
                // Price multiply to Count is not less than 0, otherwise we receive 0 in any cases
                _goodsTotal += good.Price >= good.Discount && good.Price * good.Count > 0
                            ? (good.Price -  good.Discount) * good.Count : 0;
            }

            goodInfoErrorMessage = string.Empty;
            goodsTotal = _goodsTotal; 
            return true;
        }
        else 
        {
            goodInfoErrorMessage = "Goods sequence does not contains any elements!";
            goodsTotal = 0.0m;
            return false;
        }
    }

    private bool CheckPaymentInfo (Cart cart, decimal goodsTotal, out string paymentInfoErrorMessage)
    {
        var paymentTotals = 0.0m;

        if (cart.Payments.Any())
        {
            foreach (var payment in cart.Payments)
            {
                if (!Enum.TryParse<CardTypes>(payment.CardType, out _))
                    throw new Exception("CardType is not given as enum!");

                if (!Enum.TryParse<PaymentTypes>(payment.PaymentType, out _))
                    throw new Exception("PaymentType is not given as enum!");

                paymentTotals += payment.Amount + payment.Surcharge;
            }

            if (paymentTotals < goodsTotal)
            {
                paymentInfoErrorMessage = "The cart is not paid in full!";
                return false;
            }
            else
            {
                // success
                paymentInfoErrorMessage = string.Empty;
                return true;
            }
        }
        else
        {
            paymentInfoErrorMessage = "Payments sequence does not contains any elements!";
            return false;
        }
    }
}
