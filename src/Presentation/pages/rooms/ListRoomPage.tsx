import React from 'react';
import { get } from '../../helpers/apiClient';
import { Form, useLoaderData, useNavigate } from "react-router-dom";
import { Button } from "../../components/form/Button";
import { InputDate } from "../../components/form/InputDate";

type RoomResponse = {
  id: string,
  name: string,
  description: string,
  code: string,
  photos: string[]
}

type RoomGroupResponse = {
  name: string,
  rooms: RoomResponse[]
}

export async function loader({ request }: { request: Request }) {
  return await get('/api/rooms');
}

export function ListRoomPage() {
  const roomGroupList = useLoaderData() as RoomGroupResponse[];
  const navigate = useNavigate();
  
  return (
    <div className="w-full">
      <div className="flex items-center justify-between h-16 border-b border-gray-200">
        <p className="px-6 text-xl">Acomodações</p>
        <div className='flex items-center gap-4 cursor-pointer' onClick={() => navigate('/rooms/add')}>
            <div className='text-violet-900'>
                <img className='h-8' src="/icons/add.svg" alt="" />
            </div>
          </div>
        </div>
      <div className='p-6'>
        {roomGroupList.map(roomGroup => (
          <div key={roomGroup.name} className='border-b border-gray-200 pb-2 mb-2'>
            <p>{roomGroup.name}</p>
            {roomGroup.rooms.map(room => (
              <div key={room.code} className='pl-4'>{room.code}</div>
            ))}
          </div>
        ))}
      </div>
    </div>
  );
}
