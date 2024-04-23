import { Suspense, useState } from "react";
import { get } from '../../helpers/apiClient'
import React from "react";
import { RoomGroupList } from "./RoomGroupList";

function Calendar() {
  const [searchParams, setSearchParams] = useState("");
  const roomsPromisse = async () => get(`/api/reservations/calendar?${searchParams}`)

  const searchAction = (formData) => {
    const from = formData.get('from');
    const to = formData.get('to');
    setSearchParams(`from=${from}&to=${to}`);
    console.log(`from=${from}&to=${to}`)
  }

  return(
    <div className="flex">
      <Suspense fallback={<p>Loadding...</p>}>
        <RoomGroupList roomGroupListPromisse={roomsPromisse()} searchActionCallback={searchAction} />
      </Suspense>
    </div>
  )
}

export default Calendar;