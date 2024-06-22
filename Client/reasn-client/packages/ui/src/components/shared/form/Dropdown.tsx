import React, { useEffect, useRef, useState } from "react";
import { FloatingInput } from "./Input";
import clsx from "clsx";
import { CaretDown, CaretUp } from "../../../icons";

interface DropdownProps {
  label: string;
  options: string[];
}

interface SingleDropdownProps extends DropdownProps {
  selectedOptionClass?: string;
  selectedOption: string;
  setSelectedOption: (selectedOption: string) => void;
}

interface MultiDropdownProps extends DropdownProps {
  selectedOptions: string[];
  setSelectedOptions: (selectedOptions: string[]) => void;
}

export const SearchMultiDropdown = (props: MultiDropdownProps) => {
  const { label, options, selectedOptions, setSelectedOptions } = props;

  const [search, setSearch] = useState("");

  const filteredOptions = options.filter(
    (option) =>
      option.toLowerCase().includes(search.toLowerCase()) &&
      !selectedOptions.includes(option),
  );

  const handleSelectOption = (option: string) => {
    if (selectedOptions.includes(option)) {
      setSelectedOptions(
        selectedOptions.filter((selectedOption) => selectedOption !== option),
      );
    } else {
      setSelectedOptions([...selectedOptions, option]);
    }
  };

  return (
    <div className="mt-8 flex w-full flex-col gap-2">
      <FloatingInput
        type="text"
        label={label}
        className="w-full rounded-b-none"
        defaultValue={search}
        onChange={setSearch}
      />
      <div className="flex h-16 flex-wrap justify-start gap-2 overflow-auto rounded-b-lg bg-[#232327] p-2 text-xs">
        {selectedOptions.concat(filteredOptions).map((option) => (
          <p
            key={option}
            className={clsx(
              "h-5 flex-shrink-0 cursor-pointer rounded-md px-[5px] py-[1px]",
              {
                "bg-[#4E4F75]": selectedOptions.includes(option),
              },
              {
                "bg-[#4b4e52]": !selectedOptions.includes(option),
              },
            )}
            onClick={() => handleSelectOption(option)}
          >
            {option}
          </p>
        ))}
      </div>
    </div>
  );
};

export const SingleDropdown = (props: SingleDropdownProps) => {
  const {
    label,
    options,
    selectedOption,
    selectedOptionClass,
    setSelectedOption,
  } = props;

  const [isExpanded, setIsExpanded] = useState(false);

  const ref = useRef(null);

  const handleSelectOption = (option: string) => {
    setSelectedOption(option);
  };

  const toggleExpanded = () => {
    setIsExpanded(!isExpanded);
  };

  useEffect(() => {
    const handleClickOutside = (event: MouseEvent) => {
      let currentRef = ref.current ? (ref.current as Node) : undefined;
      if (!currentRef?.contains(event.target as Node)) {
        setIsExpanded(false);
      }
    };

    document.addEventListener("click", handleClickOutside);

    return () => {
      document.removeEventListener("click", handleClickOutside);
    };
  }, []);

  return (
    <div
      className={clsx(
        "relative flex w-full cursor-pointer flex-col gap-2 rounded-lg bg-[#232327]",
        { "rounded-b-none": isExpanded },
      )}
      onClick={toggleExpanded}
      ref={ref}
    >
      <div
        className={clsx(
          "flex w-full items-center justify-between p-2",
          selectedOptionClass ?? "",
        )}
      >
        {selectedOption || "Select an option"}
        {isExpanded ? (
          <CaretUp className="ml-2 h-4 w-4 fill-white" />
        ) : (
          <CaretDown className="ml-2 h-4 w-4 fill-white" />
        )}
      </div>
      <div
        className={clsx(
          "absolute top-[100%] z-20 h-24 w-full flex-col justify-start overflow-auto rounded-b-lg bg-[#232327] text-xs",
          { hidden: !isExpanded },
          { flex: isExpanded },
        )}
      >
        {options.map((option) => (
          <p
            key={option}
            className={clsx(
              "w-full p-2",
              {
                "bg-[#4E4F75]": selectedOption === option,
              },
              {
                "bg-[#4b4e52] hover:bg-[#4E4F75]": selectedOption !== option,
              },
            )}
            onClick={() => handleSelectOption(option)}
          >
            {option}
          </p>
        ))}
      </div>
    </div>
  );
};
