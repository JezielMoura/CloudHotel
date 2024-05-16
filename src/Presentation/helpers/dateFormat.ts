export function isoToDay(value: string) {
    const date = new Date(value);
    const localeDate = new Intl.DateTimeFormat('pt-BR', {
        weekday: 'short',
        day: 'numeric',
        month: 'numeric',
        timeZone: 'UTC'
    }).format(date);

    return localeDate.replace('.', '').replace(',', '')
}

export function isoBrazilFormat(value: string) {
    let dateStr = value;
    
    if (!dateStr.includes('T')) {
        const date = new Date(dateStr);
        date.setDate(date.getDate() + 1); // Add one day
        return new Intl.DateTimeFormat('pt-BR').format(date);
    }
    const date = new Date(value);
    const localeDate = new Intl.DateTimeFormat('pt-BR').format(date);

    return localeDate;
}