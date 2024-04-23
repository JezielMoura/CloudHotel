import { use } from "react";
import React from "react";
import { RoomGroup } from "./RoomGroup";
import { Button } from "../../components/form/Button";
import { InputDate } from "../../components/form/InputDate";

export function RoomGroupList({ roomGroupListPromisse, searchActionCallback }) {
  const roomGroupList = use(roomGroupListPromisse);

  return (
    <div className="w-full">
      <div className="flex items-center justify-between h-16 border-b border-gray-200">
        <p className="px-2 text-xl">Calendário</p>
        <form action={searchActionCallback} className="flex gap-2 items-center mr-1">
          <InputDate name={'from'} />
          <p className="text-gray-500">-</p>
          <InputDate name={'to'} />
          <Button onClick={() => {}}>BUSCAR</Button>
        </form>
      </div>
      <div className="overflow-auto pb-2">
        {roomGroupList.map(roomGroup => <RoomGroup roomGroup={roomGroup} key={roomGroup.name} />)}
      </div>
    </div>
  );
}
