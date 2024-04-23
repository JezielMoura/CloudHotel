export function currencyFormat(value: any) {
  
    if (value == undefined)
      return null;
      
    const BRL = new Intl.NumberFormat('pt-BR', {
        style: 'currency',
        currency: 'BRL',
    });

    return BRL.format(value);
  }