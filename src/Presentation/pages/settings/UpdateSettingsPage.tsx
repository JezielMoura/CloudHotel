import React, { ChangeEvent, useState } from 'react';
import { Button, InputText } from "../../components/form/Form";
import { get, post } from '../../helpers/apiClient';
import { toBase64 } from '../../helpers/fileHelpers';
import { redirect, useLoaderData } from 'react-router';
import { PageHeader } from "../../components/layout/PageHeader";
import { Form } from 'react-router-dom';

type UpdateSettingsResponse = { 
  propertyName: string, 
  propertyEmail: string, 
  propertyPhone: string, 
  propertyDocument: string, 
  propertyImage: string, 
}

export async function loader({ request }: { request: Request }) { 
  return await get(`/api/settings`); 
}

export async function action({ request }: {request: Request}) {
  const formData = await request.formData();
  const requestData = Object.fromEntries(formData);
  const sucess = await post(`/api/settings`, requestData); 

  return sucess ? redirect(`/`) : null
} 

export function UpdateSettingsPage()
{
  const data = useLoaderData() as UpdateSettingsResponse;
  const [image, setImage] = useState(data.propertyImage);

  const handleImage = async (e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    if((e.target as HTMLInputElement).files?.length == 0)
      return;
    
    const file = (e.target as HTMLInputElement).files![0];
    const base64 = await toBase64(file) as string;

    setImage(base64.replace('data:', '').replace(/^.+,/, ''));
  }

  return (
    <div className="w-full">
      <PageHeader title='Configurações' />
      <Form method='post' className='p-6'>
        <InputText name={'propertyName'} value={data.propertyName} label='Nome' />
        <InputText name={'propertyEmail'} value={data.propertyEmail} label='E-mail' />
        <InputText name={'propertyPhone'} value={data.propertyPhone} label='Phone' />
        <InputText name={'propertyDocument'} value={data.propertyDocument} label='Documento' />
        <input type='file' title='logo' name='image' onChange={handleImage} multiple={false} />
        <div>
          <img src={`data:image/png;base64,${image}`} className='h-40 border border-gray-100 mb-4 mt-1' alt="Logo" />
        </div>
        <input type="hidden" name="propertyImage" value={image} />
        <Button>Salvar</Button>
      </Form>
    </div>
  )
}