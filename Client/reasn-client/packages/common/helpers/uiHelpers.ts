import { EventStatus } from "@reasn/common/enums/modelsEnums";

export const getStatusClass = (status: EventStatus) => {
  switch (status) {
    case EventStatus.APPROVED:
      return "from-green-400 text-green-400";
    case EventStatus.PENDING_APPROVAL:
      return "from-orange-400 text-orange-400";
    case EventStatus.REJECTED:
      return "from-red-400 text-red-400";
    case EventStatus.COMPLETED:
      return "from-blue-400 text-blue-400";
    case EventStatus.ONGOING:
      return "from-yellow-400 text-yellow-400";
    case EventStatus.CANCELLED:
      return "from-gray-400 text-gray-400";
    default:
      return "from-gray-400 text-gray-400";
  }
};
