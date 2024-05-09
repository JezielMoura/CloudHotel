import React from 'react';
import { post } from '../../helpers/apiClient';
import { redirect } from 'react-router';
import { Form } from 'react-router-dom';
import { PageHeader } from '../../components/layout/PageHeader'
import { Button } from "../../components/form/Button";
import { InputText } from "../../components/form/InputText";

export async function action({ request }: { request: Request }) {
  const formData = await request.formData();
  const requestData = Object.fromEntries(formData);
  const sucess = await post('/api/rooms', requestData);

  return sucess ? redirect('/rooms') : null
}

export function CreateRoomPage() {
  
  return (
    <div className="w-full">
      <PageHeader title='Nova Acomodação' />
      <Form method='post' className='p-6'>
        <label htmlFor="name">Nome</label>
        <InputText name={'name'} required={true} />
        <label htmlFor="description">Descrição</label>
        <InputText name={'description'} />
        <label htmlFor="code">Código</label>
        <InputText name={'code'} />
        <label htmlFor="quantity">Quantidade</label>
        <InputText name={'quantity'} required={true} />
        <Button>SALVAR</Button>
      </Form>
    </div>
  );
}
