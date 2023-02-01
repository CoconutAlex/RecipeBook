import React, { useState } from 'react'
import { DropdownItemProps, Form, Label, Select } from 'semantic-ui-react'

interface Props {
    options: DropdownItemProps[];
    placeholder: string;
    change: Function
}

export default function CustomFormField({ options, placeholder, change }: Props) {

    const [selectedOption, setSelectedOption] = useState(null)
    const [inputValue, setInputValue] = useState('')

    const handleSelectChange = (e: any, { value }: any) => {
        setSelectedOption(value);
        setInputValue('');
        change(value);
    }

    return (
        <Form.Field width='4'>
            <Label>Ingredient Quantity</Label>
            <Select
                options={[...options, { text: inputValue, value: inputValue }]}
                placeholder={placeholder}
                value={selectedOption || inputValue}
                onChange={handleSelectChange}
                onSearchChange={(e, { searchQuery }) => setInputValue(searchQuery)}
                search
            />
        </Form.Field>
    );
}