import { Suspense, useState } from "react";
import { get } from '../../helpers/apiClient'
import React from "react";
import { ReservationListDetails } from "./ReservationListDetails";
import { AddReservation } from "./AddReservation";

export function ReservationList() {
    const [searchParams, setSearchParams] = useState("");
    const reservationsPromisse = async () => get(`/api/reservations/?${searchParams}`)
    const roomsPromisse = async () => get(`/api/rooms`)

    const searchAction = (formData) => {
        const from = formData.get('from');
        const to = formData.get('to');
        setSearchParams(`arrivalFrom=${from}&arrivalTo=${to}`);
    }

    return(
        <div className="flex">
            <Suspense>
                <AddReservation roomsPromisse={roomsPromisse()} />
            </Suspense>
            <Suspense fallback={<p>Loadding...</p>}>
                <ReservationListDetails reservationsPromisse={reservationsPromisse()} searchActionCallback={searchAction} />
            </Suspense>
        </div>
    )
}
