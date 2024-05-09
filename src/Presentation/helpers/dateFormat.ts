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
    const date = new Date(value);
    const localeDate = new Intl.DateTimeFormat('pt-BR').format(date);

    return localeDate;
}