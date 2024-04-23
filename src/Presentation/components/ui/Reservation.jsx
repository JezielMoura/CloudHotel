import { currencyFormat } from "../../helpers/currencyFormat";
import { isoBrazilFormat } from "../../helpers/dataFormat";

export function Reservation({ reservation }) {
    return (
        <div className="flex justify-between border-b border-gray-100 mb-2 py-1">
            <p>{reservation.guestName} | in {isoBrazilFormat(reservation.arrival)} - out {isoBrazilFormat(reservation.departure)}</p>
            <p>{ currencyFormat(reservation.price)}</p>
        </div>
    )
}