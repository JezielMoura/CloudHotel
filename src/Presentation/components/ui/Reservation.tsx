import React from "react";
import { isoBrazilFormat } from "../../helpers/dateFormat";
import { Button } from "../form/Button";
import { useNavigate } from "react-router";
import { get } from "../../helpers/apiClient";

export function Reservation({ reservation }) {
	const navigate = useNavigate();

	const getFnrh = async (id: string) => {
		const content = await get(`/api/reservations/fnrh/${id}`)
    const newFrame = document.getElementById('printf') as HTMLIFrameElement;

    newFrame.srcdoc = content;
    newFrame.onload = () => newFrame.contentWindow?.print();
	}

	return (
		<div className="flex justify-between border-b border-gray-100 mb-2 py-1">
			<p><b>{isoBrazilFormat(reservation.arrival)} - {isoBrazilFormat(reservation.departure)}</b> | {reservation.guestName}</p>
			<div className="flex gap-4">
				<Button onClick={() => { getFnrh(reservation.id) }}>FNRH</Button>
				<Button onClick={() => { navigate(`/reservations/${reservation.id}`) }}>Ver</Button>
				<Button onClick={() => { navigate(`/reservations/edit/${reservation.id}`) }}>Editar</Button>
			</div>
		</div>
	)
}