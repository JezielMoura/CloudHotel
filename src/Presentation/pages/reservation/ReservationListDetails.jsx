import { Reservation } from "../../components/ui/Reservation";
import React from "react";
import { use } from "react";
import { Button } from "../../components/form/Button";
import { InputDate } from "../../components/form/InputDate";


export function ReservationListDetails({ reservationsPromisse, searchActionCallback }) {
    const reservations = use(reservationsPromisse);

    return (
        <div className="w-full">
            <div className="flex items-center justify-between h-16 border-b border-gray-200">
                <p className="px-6 text-xl">Reservas</p>
                <form action={searchActionCallback} className="flex gap-2 items-center mr-1">
                    <InputDate name={'from'} />
                    <p className="text-gray-500">-</p>
                    <InputDate name={'to'} />
                    <Button>BUSCAR</Button>
                </form>
            </div>
            <div className="w-full px-6 mt-10">
                {reservations.map(r => <Reservation reservation={r} key={r.guestName} />)}
            </div>
        </div>
    );
}
