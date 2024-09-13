using Store.Contexts;
using Store.Enums;
using Store.Models;
using Store.Services.Interfaces;

namespace Store.Services;

public class CartService(CartContext cartContext) : ICartService
{
    public async Task<IResult> CreateCart(Cart cart)
    {
        if (cart.Number > 0) return await CheckGoodInfo(cart);
        else return await Task.FromResult(Results.BadRequest("Number must be greater than 0!"));
    }

    private async Task<IResult> CheckGoodInfo(Cart cart)
    {
        var goodsTotal = 0.0m;

        foreach (var good in cart.Goods)
        {
            // Soliq requirements 🤬
            if (good.Code!.Count() != 13)
                return await Task.FromResult(Results.BadRequest($"{good.Code} is not valid as barcode!"));
            else if (good.MXIK!.Count() != 17)
                return await Task.FromResult(Results.BadRequest($"{good.MXIK} is not valid as MXIK!"));
            else if (good.UnitCode < 100000)
                return await Task.FromResult(Results.BadRequest($"{good.UnitCode} is not valid as UnitCode!"));

            // Price is not less than Discount, otherwise price like this -10...
            // Price multiply to Count is not less than 0, otherwise we receive 0 in any cases
            goodsTotal += good.Price >= good.Discount && good.Price * good.Count > 0
                        ? (good.Price - good.Discount) * good.Count : 0;
        }

        return await CheckPaymentInfo(cart, goodsTotal);
    }

    private async Task<IResult> CheckPaymentInfo (Cart cart, decimal goodsTotal)
    {
        var paymentTotals = 0.0m;

        foreach (var payment in cart.Payments)
        {
            if (!Enum.TryParse<PaymentTypes>(payment.PaymentType, out _))
                return await Task.FromResult(Results.BadRequest("PaymentType is not given as enum! \nCorrect types are: CASH CARD"));
            if (!Enum.TryParse<CardTypes>(payment.CardType, out _))
                return await Task.FromResult(Results.BadRequest("CardType is not given as enum! \nCorrect types are: HUMO, MASTERCARD, MIR, VISA, UNIONPAY, UZCARD"));

            paymentTotals += payment.Amount + payment.Surcharge;
        }

        if (paymentTotals < goodsTotal)
            return await Task.FromResult(Results.BadRequest("The cart is not paid in full!"));
        else
        {
            await cartContext.AddAsync(cart);
            return await Task.FromResult(Results.Ok(cart.Id));
        }
    }
}