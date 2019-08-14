export class Request {
  public Author: string;
  public IdeaId: string;
  public Assignee: User;
  public Comments: string;
  public CommentType: boolean;
  public CommentParentId: number;
  public IdeaPage: string;
  public PageSize: number;
  public CurrentPage: number;
  public OrderBy: string;
  public Order: string;
  public IsWatching: boolean;
  public Idea: Idea;
}

export class Response {
  public IsSuccess: boolean;
  public Message: string;
}

export class SignInResponse extends Response {
  public Author: string;
}

export class IdeasResponse extends Response {
  public Ideas: Idea[];
}

export class WatchersResponse extends Response {
  public Watchers: Watcher[];
}

export class CommentsResponse extends Response {
  public Comments: Comment[];
}

export class AlertsResponse extends Response {
  public Alerts: Alert[];
}

export class IdeaResponse extends Response {
  public Idea: Idea;
}

export class User extends Response {
  public id: string;
  public name: string;
  public email: string;
  public role: string[];
}

export class Watcher extends User {
  public IdeaId: string;
}

export class Alert extends User {
  public AlertId: string;
  public IdeaId: string;
  public IdeaName: string;
  public AlertType: string;
  public AlertDescription: string;
  public CreatedDate: string;
  public AlertFlag: boolean;
}

export class Dashboard {
  public TotalIdeas: string;
  public IdeasInAction: string;
  public IdeasPendingAction: string;
  public IdeasTrending: string;
  public MostCommented: Idea;
}

export class Comment extends User {
  public CommentId: string;
  public IdeaId: string;
  public ParentCommentId: string;
  public CommentDescription: string;
  public CreatedDate: string;
  public UpdatedDate: string;
}

export class Idea extends User {
  public IdeaId: string;
  public Type: string;
  public IdeaName: string;
  public Title: string;
  public Source: string;
  public Description: string;
  public BusinessCase: string;
  public IdealTime: string;
  public BusinessJustification: string;
  public ContactName: string;
  public ContactEmail: string;
  public ContactMobileNo: string;
  public IdeaStatus: string;
  public CreatedDate: string;
  public UpdatedDate: string;
  public WatchCount: number;
  public CommentCount: number;
  public Moderator: string;
  public Picker: string;
}
