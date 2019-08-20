export class Request {
  public author: string;
  public ideaId: string;
  public assignee: string;
  public comments: string;
  public commentType: boolean;
  public commentParentId: number;
  public ideaPage: string;
  public pageSize: number;
  public currentPage: number;
  public orderBy: string;
  public order: string;
  public isWatching: boolean;
  public idea: Idea;
  public commentId?: string;
}

export class Response {
  public isSuccess: boolean;
  public message: string;
}

export class SignInResponse extends Response {
  public author: string;
  public role: string;
}

export class IdeasResponse extends Response {
  public ideas: Idea[];
}

export class WatchersResponse extends Response {
  public watchers: Watcher[];
}

export class CommentsResponse extends Response {
  public comments: Comment[];
}

export class AlertsResponse extends Response {
  public alerts: Alert[];
}

export class IdeaResponse extends Response {
  public idea: Idea;
}

export class User extends Response {
  public id: string;
  public name: string;
  public email: string;
  public role: string[];
}

export class Watcher extends User {
  public ideaId: string;
}

export class Alert extends User {
  public alertId: string;
  public ideaId: string;
  public ideaName: string;
  public alertType: string;
  public alertDescription: string;
  public createdDate: string;
  public alertFlag: boolean;
}

export class Dashboard {
  public totalIdeas: string;
  public ideasInAction: string;
  public ideasPendingAction: string;
  public ideasTrending: string;
  public mostCommented: Idea;
}

export class Comment extends User {
  public commentId: string;
  public ideaId: string;
  public parentCommentId: string;
  public commentDescription: string;
  public createdDate: string;
  public updatedDate: string;
  public isPublic: boolean;
}

export class Idea extends User {
  public ideaId: string;
  public type: string;
  public ideaName: string;
  public title: string;
  public source: string;
  public description: string;
  public businessCase: string;
  public idealTime: string;
  public businessJustification: string;
  public contactName: string;
  public contactEmail: string;
  public contactMobileNo: string;
  public ideaStatus: string;
  public createdDate: string;
  public updatedDate: string;
  public watchCount: number;
  public commentCount: number;
  public moderatorId: string;
  public moderator: string;
  public moderatorEmail: string;
  public delegatorId: string;
  public delegator: string;
  public delegatorEmail: string;
  public pickerId: string;
  public picker: string;
  public pickerEmail: string;
  public action?: string;
  public comments?: string;
  public assignee?: string;
  public watchers?: Watcher[];
  public commentList?: Comment[];
  public commentId?: string;
  public isWatching: boolean;
  public isChild?: boolean;
  public section?: string;
  public currentSection?: string;
}
