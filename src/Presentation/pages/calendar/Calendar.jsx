import { get } from '../../helpers/apiClient'
import React from "react";
import { useLoaderData, Form } from "react-router-dom";
import { InputDate } from "../../components/form/InputDate";
import { Button } from "../../components/form/Button";
import { RoomGroup } from "./RoomGroup";

export async function loader({ request }) {
  const url = new URL(request.url);
  const from = url.searchParams.get("from") ?? '';
  const to = url.searchParams.get("to") ?? '';
  return await get(`/api/reservations/calendar?from=${from}&to=${to}`);
}

export function Calendar() {
  const roomGroupList = useLoaderData();

  return (
    <div className="w-full">
      <div className="flex items-center justify-between h-16 border-b border-gray-200">
        <p className="px-2 text-xl">Calendário</p>
        <Form id="search-form" role="search" className="flex gap-2 items-center mr-1">
          <InputDate name={'from'} />
          <p className="text-gray-500">-</p>
          <InputDate name={'to'} />
          <Button onClick={() => {}}>BUSCAR</Button>
        </Form>
      </div>
      <div className="overflow-auto pb-2">
        {roomGroupList.map(roomGroup => <RoomGroup roomGroup={roomGroup} key={roomGroup.name} />)}
      </div>
    </div>
  );
}
